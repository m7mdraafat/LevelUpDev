using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LevelUpDev.Infrastructure.Configuration;

namespace LevelUpDev.Infrastructure.Persistence;

/// <summary>
/// Factory for creating and managing Cosmos DB client and containers.
/// Uses Managed Identity for authentication (Azure best practice).
/// </summary>
public interface ICosmosDbClientFactory
{
    CosmosClient Client { get; }
    Database Database { get; }
    Container GetContainer(string containerName);
    Task InitializeDatabaseAsync(CancellationToken cancellationToken = default);
    Task<CosmosContainers> GetContainersAsync(CancellationToken cancellationToken = default);
}

public class CosmosDbClientFactory : ICosmosDbClientFactory, IAsyncDisposable
{
    private readonly CosmosClient _client;
    private readonly Database _database;
    private readonly CosmosDbConfig _config;
    private readonly ILogger<CosmosDbClientFactory> _logger;
    private readonly Dictionary<string, Container> _containers = new();

    public CosmosClient Client => _client;
    public Database Database => _database;

    public CosmosDbClientFactory(
        IOptions<CosmosDbConfig> options,
        ILogger<CosmosDbClientFactory> logger)
    {
        _config = options.Value;
        _logger = logger;

        if (!_config.Validate())
        {
            throw new InvalidOperationException("Invalid Cosmos DB configuration");
        }

        // Use Managed Identity for authentication (Azure best practice)
        // Falls back to Azure CLI credentials for local development
        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ExcludeEnvironmentCredential = false,
            ExcludeManagedIdentityCredential = false,
            ExcludeAzureCliCredential = false,
            ExcludeVisualStudioCredential = false,
            ExcludeVisualStudioCodeCredential = false
        });

        var clientOptions = new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            },
            ConnectionMode = ConnectionMode.Direct,
            MaxRetryAttemptsOnRateLimitedRequests = 9,
            MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30),
            EnableContentResponseOnWrite = false // Reduce RU consumption
        };

        _client = new CosmosClient(_config.EndpointUri, credential, clientOptions);
        _database = _client.GetDatabase(_config.DatabaseName);

        _logger.LogInformation(
            "Cosmos DB client initialized for database: {DatabaseName}",
            _config.DatabaseName);
    }

    public Container GetContainer(string containerName)
    {
        if (_containers.TryGetValue(containerName, out var container))
        {
            return container;
        }

        container = _database.GetContainer(containerName);
        _containers[containerName] = container;
        return container;
    }

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Initializing Cosmos DB database and containers...");

        try
        {
            // Create database if not exists
            await _client.CreateDatabaseIfNotExistsAsync(
                _config.DatabaseName,
                cancellationToken: cancellationToken);

            // Create containers
            var containerConfigs = new[]
            {
                _config.Containers.Users,
                _config.Containers.UserStats,
                _config.Containers.Squads,
                _config.Containers.Achievements,
                _config.Containers.Leaderboards,
                _config.Containers.Challenges,
                _config.Containers.Activities,
                _config.Containers.Notifications,
                _config.Containers.CommunityGoals
            };

            foreach (var containerConfig in containerConfigs)
            {
                await _database.CreateContainerIfNotExistsAsync(
                    containerConfig.Name,
                    containerConfig.PartitionKeyPath,
                    containerConfig.DefaultThroughput,
                    cancellationToken: cancellationToken);

                _logger.LogInformation(
                    "Container initialized: {ContainerName} with partition key: {PartitionKey}",
                    containerConfig.Name,
                    containerConfig.PartitionKeyPath);
            }

            _logger.LogInformation("Cosmos DB initialization completed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize Cosmos DB");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        _client.Dispose();
        await Task.CompletedTask;
    }

    public async Task<CosmosContainers> GetContainersAsync(CancellationToken cancellationToken = default)
    {
        // Ensure database is initialized
        await InitializeDatabaseAsync(cancellationToken);

        return new CosmosContainers
        {
            Users = GetContainer(_config.Containers.Users.Name),
            Stats = GetContainer(_config.Containers.UserStats.Name),
            Squads = GetContainer(_config.Containers.Squads.Name),
            Achievements = GetContainer(_config.Containers.Achievements.Name),
            Leaderboards = GetContainer(_config.Containers.Leaderboards.Name),
            Challenges = GetContainer(_config.Containers.Challenges.Name),
            Activities = GetContainer(_config.Containers.Activities.Name),
            Notifications = GetContainer(_config.Containers.Notifications.Name),
            CommunityGoals = GetContainer(_config.Containers.CommunityGoals.Name)
        };
    }
}

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LevelUpDev.Domain.Interfaces;
using LevelUpDev.Infrastructure.Configuration;
using LevelUpDev.Infrastructure.Persistence;
using LevelUpDev.Infrastructure.Persistence.Repositories;

namespace LevelUpDev.Infrastructure;

/// <summary>
/// Infrastructure layer dependency injection configuration.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind Cosmos DB configuration
        services.Configure<CosmosDbConfig>(configuration.GetSection("CosmosDb"));

        // Register Cosmos DB client factory
        services.AddSingleton<ICosmosDbClientFactory, CosmosDbClientFactory>();

        // Register containers (resolved lazily from factory)
        services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<ICosmosDbClientFactory>();
            return factory.GetContainersAsync().GetAwaiter().GetResult();
        });

        // Register repositories
        RegisterRepositories(services);

        return services;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        // TODO: Implement repository registrations
        // Each repository needs its specific container injected
        // Use factory pattern or named container resolution

        services.AddScoped<IUserRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<UserRepository>>();
            return new UserRepository(containers.Users, logger);
        });

        services.AddScoped<IUserStatsRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<UserStatsRepository>>();
            return new UserStatsRepository(containers.Stats, logger);
        });

        services.AddScoped<ISquadRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<SquadRepository>>();
            return new SquadRepository(containers.Squads, logger);
        });

        services.AddScoped<IAchievementRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<AchievementRepository>>();
            return new AchievementRepository(containers.Achievements, logger);
        });

        services.AddScoped<ILeaderboardRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<LeaderboardRepository>>();
            return new LeaderboardRepository(containers.Leaderboards, logger);
        });

        services.AddScoped<IChallengeRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<ChallengeRepository>>();
            return new ChallengeRepository(containers.Challenges, logger);
        });

        services.AddScoped<ICommunityGoalRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<CommunityGoalRepository>>();
            return new CommunityGoalRepository(containers.CommunityGoals, logger);
        });

        services.AddScoped<IActivityRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<ActivityRepository>>();
            return new ActivityRepository(containers.Activities, logger);
        });

        services.AddScoped<INotificationRepository>(sp =>
        {
            var containers = sp.GetRequiredService<CosmosContainers>();
            var logger = sp.GetRequiredService<ILogger<NotificationRepository>>();
            return new NotificationRepository(containers.Notifications, logger);
        });
    }
}

/// <summary>
/// Container holder for all Cosmos DB containers.
/// Resolved once at startup for efficient access.
/// </summary>
public class CosmosContainers
{
    public required Container Users { get; init; }
    public required Container Stats { get; init; }
    public required Container Squads { get; init; }
    public required Container Achievements { get; init; }
    public required Container Leaderboards { get; init; }
    public required Container Challenges { get; init; }
    public required Container Activities { get; init; }
    public required Container Notifications { get; init; }
    public required Container CommunityGoals { get; init; }
}

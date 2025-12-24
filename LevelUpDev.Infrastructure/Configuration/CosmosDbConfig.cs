namespace LevelUpDev.Infrastructure.Configuration;

/// <summary>
/// Configuration for Azure Cosmos DB connection.
/// </summary>
public class CosmosDbConfig : BaseAzureCosmosDbConfig
{
    public const string SectionName = "CosmosDb";

    /// <summary>
    /// Container configurations with partition keys.
    /// </summary>
    public CosmosContainerConfig Containers { get; set; } = new();
}

/// <summary>
/// Configuration for individual Cosmos DB containers.
/// </summary>
public class CosmosContainerConfig
{
    public ContainerInfo Users { get; set; } = new("users", "/id");
    public ContainerInfo UserStats { get; set; } = new("stats", "/userId");
    public ContainerInfo Squads { get; set; } = new("squads", "/id");
    public ContainerInfo Achievements { get; set; } = new("achievements", "/userId");
    public ContainerInfo Leaderboards { get; set; } = new("leaderboards", "/type");
    public ContainerInfo Challenges { get; set; } = new("challenges", "/date");
    public ContainerInfo Activities { get; set; } = new("activities", "/date");
    public ContainerInfo Notifications { get; set; } = new("notifications", "/userId");
    public ContainerInfo CommunityGoals { get; set; } = new("communitygoals", "/weekStart");
}

/// <summary>
/// Information about a single container.
/// </summary>
public class ContainerInfo
{
    public string Name { get; set; }
    public string PartitionKeyPath { get; set; }
    public int DefaultThroughput { get; set; } = 400;

    public ContainerInfo() : this(string.Empty, string.Empty) { }

    public ContainerInfo(string name, string partitionKeyPath, int defaultThroughput = 400)
    {
        Name = name;
        PartitionKeyPath = partitionKeyPath;
        DefaultThroughput = defaultThroughput;
    }
}

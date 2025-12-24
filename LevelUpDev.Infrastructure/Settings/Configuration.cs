namespace LevelUpDev.Infrastructure.Settings;

public class CosmosDbSettings
{
    public string AccountEndpoint { get; set; } = string.Empty;
    public string AccountKey { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public ContainerSettings Containers { get; set; } = new();
}

public class ContainerSettings
{
    public string Users { get; set; } = "users";
    public string Stats { get; set; } = "stats";
    public string Leaderboards { get; set; } = "leaderboards";
}

namespace LevelUpDev.Infrastructure.Configuration;

public abstract class BaseAzureCosmosDbConfig
{
    required public string EndpointUri { get; set;}
    required public string DatabaseName { get; set;}

    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(EndpointUri) &&
               !string.IsNullOrWhiteSpace(DatabaseName);
    }
}
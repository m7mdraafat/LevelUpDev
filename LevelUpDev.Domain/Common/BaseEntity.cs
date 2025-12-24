using System.Text.Json.Serialization;

namespace LevelUpDev.Domain.Common;

/// <summary>
/// Base entity for all Cosmos DB documents.
/// </summary>
public abstract class BaseEntity
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("_etag")]
    public string? ETag { get; set; }

    /// <summary>
    /// Gets the partition key value for this entity.
    /// Must be implemented by derived classes.
    /// </summary>
    [JsonIgnore]
    public abstract string PartitionKeyValue { get; }
}

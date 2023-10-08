using Azure;
using Azure.Data.Tables;

using RandomJokesDemo.Library.Clients;

/// <summary>
/// Represents a joke entity in the storage provider.
/// </summary>
namespace RandomJokesDemo.Library.StorageProvider;

internal class JokeEntity : JokeModel, ITableEntity
{
    /// <summary>
    /// Gets or sets the partition key for the joke entity.
    /// </summary>
    public string? PartitionKey { get; set; }

    /// <summary>
    /// Gets or sets the row key for the joke entity.
    /// </summary>
    public string? RowKey { get; set; }

    /// <summary>
    /// Gets or sets the timestamp for the joke entity.
    /// </summary>
    public DateTimeOffset? Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the ETag for the joke entity.
    /// </summary>
    public ETag ETag { get; set; }
}

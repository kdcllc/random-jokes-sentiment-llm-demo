using Azure.Data.Tables;
using Azure.Identity;

using RandomJokesDemo.Library.Clients;

namespace RandomJokesDemo.Library.StorageProvider;

internal class BlobTableStorageProvider : IStorageProvider
{
    private readonly TableClient _client;

    public BlobTableStorageProvider()
    {
        var storageUri = "https://randomjokes.table.core.windows.net";
        var tableName = "jokes";

        _client = new TableClient(
            new Uri(storageUri),
            tableName,
            new DefaultAzureCredential());
    }
    public async Task SaveAsync(JokeModel model, CancellationToken cancellationToken)
    {
        // Create the table if it doesn't already exist to verify we've successfully authenticated.
        await _client.CreateIfNotExistsAsync(cancellationToken);

        var entity = new JokeEntity
        {
            PartitionKey = $"{model.id}{model.Type}",
            RowKey = model.id.ToString(),
            id = model.id,
            Punchline = model.Punchline,
            Type = model.Type,
            Setup = model.Setup,
        };

        _client.AddEntity(entity, cancellationToken);
    }

    public async Task<IEnumerable<JokeModel>> GetAsync(CancellationToken cancellationToken)
    {
        // Create the table if it doesn't already exist to verify we've successfully authenticated.
        await _client.CreateIfNotExistsAsync(cancellationToken);

        var queryResult = _client.QueryAsync<JokeEntity>(x => x.Punchline != "", cancellationToken: cancellationToken);

        var pages = queryResult.AsPages();

        var result = new List<JokeModel>();

        await foreach (var page in pages.WithCancellation(cancellationToken))
        {
            foreach (var v in page.Values)
            {
                result.Add(new JokeModel
                {
                    id = v.id,
                    Punchline = v.Punchline,
                    Setup = v.Setup,
                    Type = v.Type
                });
            }
        }

        return result;
    }
}

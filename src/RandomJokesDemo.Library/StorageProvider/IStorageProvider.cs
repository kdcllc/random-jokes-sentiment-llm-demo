using RandomJokesDemo.Library.Clients;

namespace RandomJokesDemo.Library.StorageProvider;

/// <summary>
/// Interface for a storage provider that can save and retrieve joke models.
/// </summary>
public interface IStorageProvider
{
    /// <summary>
    /// Saves a joke model to the storage provider.
    /// </summary>
    /// <param name="model">The joke model to save.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    Task SaveAsync(JokeModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all joke models from the storage provider.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    Task<IEnumerable<JokeModel>> GetAsync(CancellationToken cancellationToken);
}
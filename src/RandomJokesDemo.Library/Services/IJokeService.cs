using RandomJokesDemo.Library.Clients;

namespace RandomJokesDemo.Library.Services;

public interface IJokeService
{
    Task<(JokeModel, string)> GetAndSaveAsync(CancellationToken cancellationToken);

    Task<IEnumerable<JokeModel>> GetStoredAsync(CancellationToken cancellationToken);
}
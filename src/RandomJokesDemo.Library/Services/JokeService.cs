using Microsoft.Extensions.Logging;

using RandomJokesDemo.Library.Clients;
using RandomJokesDemo.Library.StorageProvider;

namespace RandomJokesDemo.Library.Services;

internal class JokeService : IJokeService
{
    private readonly IRandomJokesClient _jokesClient;
    private readonly IStorageProvider _storageClient;
    private readonly ISentementService _sentementService;
    private readonly ILogger<JokeService> _logger;

    public JokeService(
        IRandomJokesClient jokesClient,
        IStorageProvider storageProvider,
        ISentementService sentementService,
        ILogger<JokeService> logger)
    {
        _jokesClient = jokesClient;
        _storageClient = storageProvider;
        _sentementService = sentementService;

        _logger = logger;
    }

    public async Task<(JokeModel, string)> GetAndSaveAsync(CancellationToken cancellationToken)
    {
        var joke = await _jokesClient.GetJoke(cancellationToken);

        await _storageClient.SaveAsync(joke, cancellationToken);

        var sentement = await _sentementService.GetResultAsync($"{joke.Setup} {joke.Punchline}");

        return (joke, sentement);
    }

    public async Task<IEnumerable<JokeModel>> GetStoredAsync(CancellationToken cancellationToken)
    {
        return await _storageClient.GetAsync(cancellationToken);
    }
}

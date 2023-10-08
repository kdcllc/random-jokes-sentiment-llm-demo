using System.Net.Http.Json;

namespace RandomJokesDemo.Library.Clients;

internal class RandomJokesClient : IRandomJokesClient
{
    private readonly HttpClient _client;

    public RandomJokesClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<JokeModel?> GetJoke(CancellationToken cancellationToken)
    {
        return await _client.GetFromJsonAsync<JokeModel>("https://official-joke-api.appspot.com/random_joke", cancellationToken);
    }
}

namespace RandomJokesDemo.Library.Clients;

public interface IRandomJokesClient
{
    Task<JokeModel?> GetJoke(CancellationToken cancellationToken);
}
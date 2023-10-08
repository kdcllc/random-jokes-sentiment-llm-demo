namespace RandomJokesDemo.Library.Services;

public interface ISentementService
{
    Task<string> GetResultAsync(string input);
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RandomJokesDemo.Library.Clients;
using RandomJokesDemo.Library.Services;

namespace RandomJokesDemo.Pages;

public class IndexModel : PageModel
{
    private readonly IJokeService _jokeService;
    private readonly ILogger<IndexModel> _logger;

    public IEnumerable<JokeModel> Jokes { get; set; }
    public JokeModel Joke { get; set; }
    public string Sentiment { get; set; }

    public IndexModel(
        IJokeService jokeService, ILogger<IndexModel> logger)
    {
        _jokeService = jokeService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        var joke = await _jokeService.GetAndSaveAsync(cancellationToken);

        var jokes = await _jokeService.GetStoredAsync(cancellationToken);

        Joke = joke.Item1;
        Jokes = jokes;
        Sentiment = joke.Item2;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        return RedirectToPage("./Index");
    }
}
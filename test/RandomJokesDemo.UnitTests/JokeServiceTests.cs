using Microsoft.Extensions.Logging;
using Moq;
using RandomJokesDemo.Library.Clients;
using RandomJokesDemo.Library.Services;
using RandomJokesDemo.Library.StorageProvider;
using Xunit.Abstractions;

namespace RandomJokesDemo.UnitTests;

public class JokeServiceTests
{
    private readonly Mock<IRandomJokesClient> _jokesClientMock;
    private readonly Mock<IStorageProvider> _storageClientMock;
    private readonly Mock<ISentementService> _sentementServiceMock;
    private readonly ILogger<JokeService>? _loggerMock;
    private readonly JokeService _jokeService;

    public JokeServiceTests(ITestOutputHelper output)
    {
        _jokesClientMock = new Mock<IRandomJokesClient>();
        _storageClientMock = new Mock<IStorageProvider>();
        _sentementServiceMock = new Mock<ISentementService>();
           
        _loggerMock = new LoggerFactory().CreateLogger<JokeService>();

        _jokeService = new JokeService(
            _jokesClientMock.Object,
            _storageClientMock.Object,
            _sentementServiceMock.Object,
            _loggerMock);
    }

    [Fact]
    public async Task GetAndSaveAsync_ShouldReturnJokeAndSentiment_WhenCalled()
    {
        // Arrange
        var joke = new JokeModel { Setup = "Why did the chicken cross the road?", Punchline = "To get to the other side" };
        var sentiment = "positive";
        _jokesClientMock.Setup(x => x.GetJoke(It.IsAny<CancellationToken>())).ReturnsAsync(joke);
        _sentementServiceMock.Setup(x => x.GetResultAsync($"{joke.Setup} {joke.Punchline}")).ReturnsAsync(sentiment);

        // Act
        var result = await _jokeService.GetAndSaveAsync(CancellationToken.None);

        // Assert
        Assert.Equal(joke, result.Item1);
        Assert.Equal(sentiment, result.Item2);
        _jokesClientMock.Verify(x => x.GetJoke(It.IsAny<CancellationToken>()), Times.Once);
        _storageClientMock.Verify(x => x.SaveAsync(joke, It.IsAny<CancellationToken>()), Times.Once);
        _sentementServiceMock.Verify(x => x.GetResultAsync($"{joke.Setup} {joke.Punchline}"), Times.Once);
    }

    [Fact]
    public async Task GetStoredAsync_ShouldReturnStoredJokes_WhenCalled()
    {
        // Arrange
        var jokes = new[] { new JokeModel { Setup = "Why did the chicken cross the road?", Punchline = "To get to the other side" } };
        _storageClientMock.Setup(x => x.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(jokes);

        // Act
        var result = await _jokeService.GetStoredAsync(CancellationToken.None);

        // Assert
        Assert.Equal(jokes, result);
        _storageClientMock.Verify(x => x.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
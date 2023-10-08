using Azure.AI.OpenAI;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RandomJokesDemo.Library.Services;

internal class SentementService : ISentementService
{
    private OpenAiOptions _options;
    private ILogger<SentementService> _logger;

    public SentementService(
        IOptions<OpenAiOptions> options,
        ILogger<SentementService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> GetResultAsync(string input)
    {
        var prompt =
        """
            Decide whether a joke's sentiment is positive, neutral, sarcastic, self-Deprecating, clever, nonsensical, satirical or negative.

            joke: "{0}"
            Sentiment:
            """;

        var completionOptions = new CompletionsOptions
        {
            Prompts = { string.Format(prompt, input) },
            MaxTokens = 60,
            Temperature = 0f,
            FrequencyPenalty = 0.5f,
            PresencePenalty = 0.0f,
            NucleusSamplingFactor = 1 // Top P
        };

        var endpoint = new Uri(_options.Endpoint);
        var credentials = new Azure.AzureKeyCredential(_options.Key);
        var openAIClient = new OpenAIClient(endpoint, credentials);

        Completions response = await openAIClient.GetCompletionsAsync(_options.DeploymentId, completionOptions);

        var result = response.Choices.First();

        _logger.LogInformation(result.Text);

        return result.Text;
    }
}

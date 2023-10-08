using System.Text.Json.Serialization;

namespace RandomJokesDemo.Library.Clients;

public class JokeModel
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("setup")]
    public string? Setup { get; set; }

    [JsonPropertyName("punchline")]
    public string? Punchline { get; set; }

    [JsonPropertyName("id")]
    public int id { get; set; }
}

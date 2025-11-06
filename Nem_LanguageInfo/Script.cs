using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  public class Script {
    [JsonPropertyName("code")]
    public string Code { get; internal set; }

    [JsonPropertyName("number")]
    public int Number { get; internal set; }

    [JsonPropertyName("englishName")]
    public string Name { get; internal set; }

    [JsonPropertyName("alias")]
    public string Alias { get; internal set; }

    [JsonPropertyName("age")]
    public int Age { get; internal set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; internal set; }

    [JsonPropertyName("direction")]
    public string Directionality { get; internal set; }
  }
}

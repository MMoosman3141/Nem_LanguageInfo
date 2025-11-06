using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  public class Script {
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("englishName")]
    public string NameName { get; set; }

    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("direction")]
    public string Directionality { get; set; }
  }
}

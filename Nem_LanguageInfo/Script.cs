using System.Text.Json.Serialization;

namespace Nem_LanguageInfo; 

public sealed class Script {
  [JsonPropertyName("code")]
  public string Code { get; private set; }

  [JsonPropertyName("number")]
  public int Number { get; private set; }

  [JsonPropertyName("englishName")]
  public string Name { get; private set; }

  [JsonPropertyName("alias")]
  public string Alias { get; private set; }

  [JsonPropertyName("age")]
  public int Age { get; private set; }

  [JsonPropertyName("date")]
  public string Date { get; private set; }

  [JsonPropertyName("direction")]
  public string Directionality { get; private set; }

  [JsonConstructor]
  public Script(string code, int number, string name,
    string alias, int age, string date, string directionality) {
    Code = code;
    Number = number;
    Name = name;
    Alias = alias;
    Age = age;
    Date = date;
    Directionality = directionality;
  }
}

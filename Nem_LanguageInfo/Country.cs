using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  internal class Country {
    public string Name { get; private set; }
    public string Alpha2 { get; private set; }
    public string Alpha3 { get; private set; }
    public string PrimaryLanguage { get; private set; }

    [JsonConstructor]
    public Country(string name, string alpha2, string alpha3, string primaryLanguage) {
      Name = name;
      Alpha2 = alpha2;
      Alpha3 = alpha3;
      PrimaryLanguage = primaryLanguage;
    }
  }
}

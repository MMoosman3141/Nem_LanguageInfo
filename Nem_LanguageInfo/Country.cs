using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  [method: JsonConstructor]
  internal class Country(string name, string alpha2, string alpha3, string primaryLanguage) {
    public string Name { get; private set; } = name;
    public string Alpha2 { get; private set; } = alpha2;
    public string Alpha3 { get; private set; } = alpha3;
    public string PrimaryLanguage { get; private set; } = primaryLanguage;
  }
}

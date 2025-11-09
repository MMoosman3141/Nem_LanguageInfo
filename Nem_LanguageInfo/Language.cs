using System.Text.Json.Serialization;

namespace Nem_LanguageInfo;

public sealed class Language() {
  [JsonPropertyName("name")]
  public string Name { get; private set; }
  
  [JsonPropertyName("part1Code")]
  public string Part1Code { get; private set; }
  
  [JsonPropertyName("part2TCode")]
  public string Part2TCode { get; private set; }
  
  [JsonPropertyName("part2BCode")]
  public string Part2BCode { get; private set; }
  
  [JsonPropertyName("part3Code")]
  public string Part3Code { get; private set; }
 
  [JsonPropertyName("scope")]
  public string Scope { get; private set; }
  
  [JsonPropertyName("type")]
  public string Type { get; private set; }
  
  [JsonPropertyName("comment")]
  public string Comment { get; private set; }
  
  [JsonPropertyName("defaultScript")]
  public string DefaultScript { get; private set; }

  [JsonPropertyName("aliases")]
  public List<string> Aliases { get; private set; }

  [JsonIgnore]
  public Script Script { get => Scripts.Instance.GetFromCode(DefaultScript); }

  [JsonConstructor]
  public Language(
    string name, string part1Code, string part2TCode, 
    string part2BCode, string part3Code, string scope, 
    string type, string comment, string defaultScript, List<string> aliases) :
    this() {

    this.Name = name;
    this.Part1Code = part1Code;
    this.Part2TCode = part2TCode;
    this.Part2BCode = part2BCode;
    this.Part3Code = part3Code;
    this.Scope = scope;
    this.Type = type;
    this.Comment = comment;
    this.DefaultScript = defaultScript;
    this.Aliases = aliases ?? [];
  }
}

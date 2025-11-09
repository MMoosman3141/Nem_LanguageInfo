using System.Text.Json.Serialization;

namespace Nem_LanguageInfo;

/// <summary>
/// Represents a language with ISO codes, scope, type, and related metadata.
/// </summary>
public sealed class Language() {
  /// <summary>
  /// Gets the name of the language.
  /// </summary>
  [JsonPropertyName("name")]
  public string Name { get; private set; }

  /// <summary>
  /// Gets the ISO 639-1 code for the language.
  /// </summary>
  [JsonPropertyName("part1Code")]
  public string Part1Code { get; private set; }

  /// <summary>
  /// Gets the ISO 639-2/T code for the language.
  /// </summary>
  [JsonPropertyName("part2TCode")]
  public string Part2TCode { get; private set; }

  /// <summary>
  /// Gets the ISO 639-2/B code for the language.
  /// </summary>
  [JsonPropertyName("part2BCode")]
  public string Part2BCode { get; private set; }

  /// <summary>
  /// Gets the ISO 639-3 code for the language.
  /// </summary>
  [JsonPropertyName("part3Code")]
  public string Part3Code { get; private set; }

  /// <summary>
  /// Gets the scope of the language.
  /// </summary>
  [JsonPropertyName("scope")]
  public string Scope { get; private set; }

  /// <summary>
  /// Gets the type of the language.
  /// </summary>
  [JsonPropertyName("type")]
  public string Type { get; private set; }

  /// <summary>
  /// Gets the comment or description for the language.
  /// </summary>
  [JsonPropertyName("comment")]
  public string Comment { get; private set; }

  /// <summary>
  /// Gets the default script code for the language.
  /// </summary>
  [JsonPropertyName("defaultScript")]
  public string DefaultScript { get; private set; }

  /// <summary>
  /// Gets the list of aliases for the language.
  /// </summary>
  [JsonPropertyName("aliases")]
  public List<string> Aliases { get; private set; }

  /// <summary>
  /// Gets the script object associated with the default script code.
  /// </summary>
  [JsonIgnore]
  public Script Script { get => Scripts.Instance.GetFromCode(DefaultScript); }

  /// <summary>
  /// Initializes a new instance of the <see cref="Language"/> class.
  /// </summary>
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

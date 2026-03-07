using System.Text.Json.Serialization;

namespace Nem_LanguageInfo;

/// <summary>
/// Represents a writing script with associated metadata such as code, number, name, alias, age, date, and directionality.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Script"/> class.
/// </remarks>
/// <param name="code">The script code.</param>
/// <param name="number">The script number.</param>
/// <param name="name">The English name of the script.</param>
/// <param name="alias">The alias of the script.</param>
/// <param name="age">The age of the script.</param>
/// <param name="date">The date associated with the script.</param>
/// <param name="directionality">The directionality of the script.</param>
/// <param name="wordSeparation">The word separator character for the script (empty string if none).</param>
[method: JsonConstructor]
public sealed class Script(string code, int number, string name,
  string alias, int age, string date, string directionality, string wordSeparation) {
  /// <summary>
  /// Gets the script code.
  /// </summary>
  [JsonPropertyName("code")]
  public string Code { get; private set; } = code;

  /// <summary>
  /// Gets the script number.
  /// </summary>
  [JsonPropertyName("number")]
  public int Number { get; private set; } = number;

  /// <summary>
  /// Gets the English name of the script.
  /// </summary>
  [JsonPropertyName("englishName")]
  public string Name { get; private set; } = name;

  /// <summary>
  /// Gets the alias of the script.
  /// </summary>
  [JsonPropertyName("alias")]
  public string Alias { get; private set; } = alias;

  /// <summary>
  /// Gets the age of the script.
  /// </summary>
  [JsonPropertyName("age")]
  public int Age { get; private set; } = age;

  /// <summary>
  /// Gets the date associated with the script.
  /// </summary>
  [JsonPropertyName("date")]
  public string Date { get; private set; } = date;

  /// <summary>
  /// Gets the directionality of the script.
  /// </summary>
  [JsonPropertyName("direction")]
  public string Directionality { get; private set; } = directionality;

  /// <summary>
  /// Gets the word separation character used in the script.
  /// Empty string for scripts that do not use a word separator.
  /// </summary>
  [JsonPropertyName("wordSeparation")]
  public string WordSeparation { get; private set; } = wordSeparation;
}

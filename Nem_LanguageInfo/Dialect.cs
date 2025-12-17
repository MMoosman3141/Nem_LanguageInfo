using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nem_LanguageInfo;

/// <summary>
/// Represents a language dialect, including language, region, script, and variant information.
/// </summary>
public class Dialect {
  private const string LANGUAGE_PATTERN = @"(?<language>[a-z]{2,3})";
  private const string SCRIPT_PATTERN = @"(-(?<script>[a-z]{4}))?";
  private const string REGION_PATTERN = @"(-(?<region>[a-z]{2}|\d{3}))?";
  private const string VARIANT_PATTERN = @"(-(?<variant>[a-z0-9]{5,8}|\d[a-z0-9]{3}))?";

  private static readonly Regex bc947Rgx = new($@"^{LANGUAGE_PATTERN}{SCRIPT_PATTERN}{REGION_PATTERN}{VARIANT_PATTERN}$",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

  /// <summary>
  /// Gets or sets the language component of the dialect.
  /// </summary>
  public Language Language { get; set; }

  /// <summary>
  /// Gets or sets the region (area) component of the dialect.
  /// </summary>
  public Area Region { get; set; }

  /// <summary>
  /// Gets or sets the script component of the dialect.
  /// </summary>
  public Script Script { get; set; }

  /// <summary>
  /// Gets or sets the variant component of the dialect.
  /// </summary>
  public string Variant { get; set; } = null;

  /// <summary>
  /// Initializes a new instance of the <see cref="Dialect"/> class with the specified language, region, script, and variant.
  /// </summary>
  /// <param name="language">The language component of the dialect.</param>
  /// <param name="region">The region (area) component of the dialect. Optional.</param>
  /// <param name="script">The script component of the dialect. Optional.</param>
  /// <param name="variant">The variant component of the dialect. Optional.</param>
  public Dialect(Language language, Area region = null, Script script = null, string variant = null) {
    Language = language;
    Region = region ?? null;
    Script = script ?? null;
    Variant = variant;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Dialect"/> class with the specified language, region, script, and variant codes.
  /// </summary>
  /// <param name="languageCode">The language code (ISO 639) for the dialect.</param>
  /// <param name="regionCode">The region (area) code (ISO 3166 or UN M49) for the dialect. Optional.</param>
  /// <param name="scriptCode">The script code (ISO 15924) for the dialect. Optional.</param>
  /// <param name="variant">The variant component of the dialect. Optional.</param>
  public Dialect(string languageCode, string regionCode = null, string scriptCode = null, string variant = null) {
    Language = Languages.Instance.GetLanguage(languageCode) ?? throw new ArgumentException($"Invalid language code: {languageCode}");
    Region = !string.IsNullOrWhiteSpace(regionCode) ? Regions.Instance.GetArea(regionCode) ?? throw new ArgumentException($"Invalid region code: {regionCode}") : null;
    Script = !string.IsNullOrWhiteSpace(scriptCode) ? Scripts.Instance.GetFromCode(scriptCode) ?? throw new ArgumentException($"Invalid script code: {scriptCode}") : null;
    Variant = variant;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Dialect"/> class from a BCP 47 language tag.
  /// </summary>
  /// <param name="bcp47Code">The BCP 47 language tag representing the dialect (e.g., "en-US", "zh-Hans-CN").</param>
  /// <exception cref="ArgumentException">Thrown if the BCP 47 code is invalid or cannot be parsed.</exception>
  public Dialect(string bcp47Code) {
    Match match = bc947Rgx.Match(bcp47Code);
    if (!match.Success) {
      throw new ArgumentException($"Invalid BCP 47 code: {bcp47Code}");
    }
    string languageCode = match.Groups["language"].Value;
    string scriptCode = match.Groups["script"].Success ? match.Groups["script"].Value : null;
    string regionCode = match.Groups["region"].Success ? match.Groups["region"].Value : null;
    string variant = match.Groups["variant"].Success ? match.Groups["variant"].Value : null;
    Language = Languages.Instance.GetLanguage(languageCode) ?? throw new ArgumentException($"Invalid language code: {languageCode}");
    Script = scriptCode is not null ? Scripts.Instance.GetFromCode(scriptCode) ?? throw new ArgumentException($"Invalid script code: {scriptCode}") : null;
    Region = regionCode is not null ? Regions.Instance.GetArea(regionCode) ?? throw new ArgumentException($"Invalid region code: {regionCode}") : null;
    Variant = variant;
  }

  /// <summary>
  /// Returns the BCP 47 string representation of the dialect, including language, script, region, and variant if present.
  /// </summary>
  public override string ToString() {
    StringBuilder sb = new(Language.Part1Code ?? Language.Part3Code);
    if (Script is not null) {
      sb.Append('-').Append(Script.Code);
    }
    if (Region is not null) {
      sb.Append('-').Append(Region.Alpha2Code ?? Region.Alpha3Code ?? Region.M49Code);
    }
    if (!string.IsNullOrEmpty(Variant)) {
      sb.Append('-').Append(Variant);
    }
    return sb.ToString();
  }

}

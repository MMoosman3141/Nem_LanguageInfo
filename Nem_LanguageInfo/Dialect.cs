using System.Text;
using System.Text.RegularExpressions;

namespace Nem_LanguageInfo;

/// <summary>
/// Represents a language dialect, including language, region, script, and variant information.
/// </summary>
public partial class Dialect {
  private const string LANGUAGE_PATTERN = @"(?<language>[a-z]{2,3})";
  private const string SCRIPT_PATTERN = @"(-(?<script>[a-z]{4}))?";
  private const string REGION_PATTERN = @"(-(?<region>[a-z]{2}|\d{3}))?";
  private const string VARIANT_PATTERN = @"(-(?<variant>[a-z0-9]{5,8}|\d[a-z0-9]{3}))?";

  [GeneratedRegex($@"^{LANGUAGE_PATTERN}{SCRIPT_PATTERN}{REGION_PATTERN}{VARIANT_PATTERN}$", RegexOptions.IgnoreCase)]
  // Keeping the parser regex generated and cached avoids repeated regex construction
  // while ensuring every BCP 47 parse follows a single canonical pattern.
  private static partial Regex Bcp47Regex();

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
    // Fail fast so callers get a clear contract violation instead of null-driven failures later.
    ArgumentNullException.ThrowIfNull(language);

    Language = language;
    Region = region;
    Script = script ?? language.Script;
    Variant = variant;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Dialect"/> class with the specified language, region, script, and variant codes.
  /// </summary>
  /// <param name="languageCode">The language code (ISO 639) for the dialect.</param>
  /// <param name="regionCode">The region (area) code (ISO 3166 or UN M49) for the dialect. Optional.</param>
  /// <param name="scriptCode">The script code (ISO 15924) for the dialect. Optional.</param>
  /// <param name="variant">The variant component of the dialect. Optional.</param>
  [Obsolete("Use Dialect.FromParts(...) to make caller intent explicit.")]
  public Dialect(string languageCode, string regionCode = null, string scriptCode = null, string variant = null) {
    Dialect dialect = FromParts(languageCode, regionCode, scriptCode, variant);

    Language = dialect.Language;
    Region = dialect.Region;
    Script = dialect.Script;
    Variant = dialect.Variant;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Dialect"/> class from a BCP 47 language tag.
  /// </summary>
  /// <param name="bcp47Code">The BCP 47 language tag representing the dialect (e.g., "en-US", "zh-Hans-CN").</param>
  /// <exception cref="ArgumentException">Thrown if the BCP 47 code is invalid or cannot be parsed.</exception>
  [Obsolete("Use Dialect.Parse(...) or Dialect.TryParse(...) to make caller intent explicit.")]
  public Dialect(string bcp47Code) {
    // Centralized parsing keeps constructor and Parse/TryParse behavior identical.
    Dialect parsed = Parse(bcp47Code);
    Language = parsed.Language;
    Region = parsed.Region;
    Script = parsed.Script;
    Variant = parsed.Variant;
  }

  /// <summary>
  /// Creates a <see cref="Dialect"/> from a full BCP 47 language tag.
  /// </summary>
  /// <param name="bcp47Code">The BCP 47 language tag.</param>
  /// <returns>A <see cref="Dialect"/> representing the parsed language tag.</returns>
  public static Dialect Parse(string bcp47Code) {
    // Named factory makes caller intent explicit and avoids constructor-overload ambiguity.
    (Language language, Area region, Script script, string variant) = ResolveFromBcp47(bcp47Code);    
    return new(language, region, script, variant);
  }

  /// <summary>
  /// Tries to parse a BCP 47 language tag into a <see cref="Dialect"/>.
  /// </summary>
  /// <param name="bcp47Code">The BCP 47 language tag.</param>
  /// <param name="dialect">When this method returns, contains the parsed dialect if successful; otherwise null.</param>
  /// <returns>True when parsing succeeds; otherwise false.</returns>
  public static bool TryParse(string bcp47Code, out Dialect dialect) {
    // Non-throwing parse path lets callers validate user input without exception-based control flow.
    try {
      dialect = Parse(bcp47Code);
      return true;
    } catch (ArgumentException) {
      dialect = null;
      return false;
    }
  }

  /// <summary>
  /// Creates a <see cref="Dialect"/> from individual BCP 47 components.
  /// </summary>
  /// <param name="languageCode">The language code (ISO 639).</param>
  /// <param name="regionCode">The region code (ISO 3166 or UN M49). Optional.</param>
  /// <param name="scriptCode">The script code (ISO 15924). Optional.</param>
  /// <param name="variant">The variant component. Optional.</param>
  /// <returns>A <see cref="Dialect"/> built from provided component values.</returns>
  public static Dialect FromParts(string languageCode, string regionCode = null, string scriptCode = null, string variant = null) {
    // Named factory communicates that inputs are components rather than a precomposed tag.
    // Validate once at the boundary to keep downstream resolution logic simple and deterministic.
    if (string.IsNullOrWhiteSpace(languageCode)) {
      throw new ArgumentException("Language code cannot be null or whitespace.", nameof(languageCode));
    }

    Language language = ResolveLanguage(languageCode);    
    Area region = !string.IsNullOrWhiteSpace(regionCode) ? Regions.Instance.GetArea(regionCode) ?? throw new ArgumentException($"Invalid region code: {regionCode}", nameof(regionCode)) : null;
    Script script = ResolveScript(scriptCode, language.Script);
    return new Dialect(language, region, script, variant);
  }

  /// <summary>
  /// Resolves and validates all components from a BCP 47 language tag.
  /// </summary>
  /// <remarks>
  /// Keeping BCP 47 parsing and component resolution in one place prevents behavioral drift
  /// between constructor and Parse/TryParse entry points.
  /// </remarks>
  private static (Language Language, Area Region, Script Script, string Variant) ResolveFromBcp47(string bcp47Code) {
    if (string.IsNullOrWhiteSpace(bcp47Code)) {
      throw new ArgumentException("BCP 47 code cannot be null or whitespace.", nameof(bcp47Code));
    }

    Match match = Bcp47Regex().Match(bcp47Code);
    if (!match.Success) {
      throw new ArgumentException($"Invalid BCP 47 code: {bcp47Code}", nameof(bcp47Code));
    }

    string languageCode = match.Groups["language"].Value;
    string scriptCode = match.Groups["script"].Success ? match.Groups["script"].Value : null;
    string regionCode = match.Groups["region"].Success ? match.Groups["region"].Value : null;
    string variant = match.Groups["variant"].Success ? match.Groups["variant"].Value : null;

    Language language = ResolveLanguage(languageCode);
    Script script = ResolveScript(scriptCode, language.Script);
    Area region = regionCode is not null ? Regions.Instance.GetArea(regionCode) ?? throw new ArgumentException($"Invalid region code: {regionCode}", nameof(bcp47Code)) : null;

    return (language, region, script, variant);
  }

  /// <summary>
  /// Resolves the language code to a known language.
  /// </summary>
  /// <remarks>
  /// Centralizing this check ensures all constructors treat unknown codes consistently
  /// by rejecting sentinel "und" results unless the caller explicitly requested "und".
  /// </remarks>
  private static Language ResolveLanguage(string languageCode) {
    Language language = Languages.Instance.GetLanguage(languageCode);
    if (language.Part3Code.Equals("und", StringComparison.OrdinalIgnoreCase) &&
      !string.Equals(languageCode, "und", StringComparison.OrdinalIgnoreCase)) {
      throw new ArgumentException($"Invalid language code: {languageCode}", nameof(languageCode));
    }

    return language;
  }

  /// <summary>
  /// Resolves script code and applies a fallback script when script input is not provided.
  /// </summary>
  /// <remarks>
  /// This keeps script resolution policy in one place and prevents unknown script sentinels
  /// from silently leaking into valid dialect instances.
  /// </remarks>
  private static Script ResolveScript(string scriptCode, Script fallbackScript) {
    if (string.IsNullOrWhiteSpace(scriptCode)) {
      return fallbackScript;
    }

    Script script = Scripts.Instance.GetFromCode(scriptCode);
    if (script.Code.Equals("Zzzz", StringComparison.OrdinalIgnoreCase) &&
      !scriptCode.Equals("Zzzz", StringComparison.OrdinalIgnoreCase)) {
      throw new ArgumentException($"Invalid script code: {scriptCode}", nameof(scriptCode));
    }

    return script;
  }

  /// <summary>
  /// Returns the BCP 47 string representation of the dialect, including language, script, region, and variant if present.
  /// </summary>
  public override string ToString() {
    // Default output stays language-only to preserve backward-compatible behavior.
    return ToString(DialectOptions.None);
  }

  /// <summary>
  /// Returns the BCP 47 string representation of the dialect using the specified <see cref="DialectOptions"/>.
  /// </summary>
  /// <param name="options">Options that control which components (language, script, region, variant) are included in the output.</param>
  /// <returns>A BCP 47 language tag string representing the dialect.</returns>
  public string ToString(DialectOptions options) {
    // Build in ordered segments so formatting is stable regardless of option combinations.
    StringBuilder sb = new();

    PopulateLanguage(sb, options);

    if (options.HasFlag(DialectOptions.Script) && Script is not null) {
      sb.Append('-').Append(Script.Code);
    }

    PopulateRegion(sb, options);

    if (options.HasFlag(DialectOptions.Variant) && !string.IsNullOrWhiteSpace(Variant)) {
      sb.Append('-').Append(Variant);
    }

    return sb.ToString();
  }

  /// <summary>
  /// Appends the most appropriate language code to the output.
  /// </summary>
  /// <remarks>
  /// This guarantees a usable language segment even when data is incomplete,
  /// falling back to "und" to keep output standards-compliant.
  /// </remarks>
  private void PopulateLanguage(StringBuilder sb, DialectOptions options) {
    if (options.HasFlag(DialectOptions.LanguagePreferPart3)) {
      if (!string.IsNullOrWhiteSpace(Language?.Part3Code)) {
        sb.Append(Language.Part3Code);
      } else if (!string.IsNullOrWhiteSpace(Language?.Part1Code)) {
        sb.Append(Language.Part1Code);
      } else {
        sb.Append("und");
      }
    } else {
      if (!string.IsNullOrWhiteSpace(Language?.Part1Code)) {
        sb.Append(Language.Part1Code);
      } else if (!string.IsNullOrWhiteSpace(Language?.Part3Code)) {
        sb.Append(Language.Part3Code);
      } else {
        sb.Append("und");
      }
    }
  }

  /// <summary>
  /// Appends region using a deterministic preference order from the selected options.
  /// </summary>
  /// <remarks>
  /// Keeping region fallback logic in one method prevents inconsistent formatting behavior
  /// between callers and ensures option precedence is applied uniformly.
  /// </remarks>
  private void PopulateRegion(StringBuilder sb, DialectOptions options) {
    if (options.HasFlag(DialectOptions.Region) && Region is not null) {
      if (options.HasFlag(DialectOptions.RegionPreferUnM49)) {
        if (!string.IsNullOrWhiteSpace(Region.M49Code)) {
          sb.Append('-').Append(Region.M49Code);
        } else {
          if (options.HasFlag(DialectOptions.RegionPreferAlpha3)) {
            if (!string.IsNullOrWhiteSpace(Region.Alpha3Code)) {
              sb.Append('-').Append(Region.Alpha3Code);
            } else if (!string.IsNullOrWhiteSpace(Region.Alpha2Code)) {
              sb.Append('-').Append(Region.Alpha2Code);
            }
          } else {
            if (!string.IsNullOrWhiteSpace(Region.Alpha2Code)) {
              sb.Append('-').Append(Region.Alpha2Code);
            } else if (!string.IsNullOrWhiteSpace(Region.Alpha3Code)) {
              sb.Append('-').Append(Region.Alpha3Code);
            }
          }
        }
      } else {
        if (options.HasFlag(DialectOptions.RegionPreferAlpha3)) {
          if (!string.IsNullOrWhiteSpace(Region.Alpha3Code)) {
            sb.Append('-').Append(Region.Alpha3Code);
          } else if (!string.IsNullOrWhiteSpace(Region.Alpha2Code)) {
            sb.Append('-').Append(Region.Alpha2Code);
          } else if (!string.IsNullOrWhiteSpace(Region.M49Code)) {
            sb.Append('-').Append(Region.M49Code);
          }
        } else {
          if (!string.IsNullOrWhiteSpace(Region.Alpha2Code)) {
            sb.Append('-').Append(Region.Alpha2Code);
          } else if (!string.IsNullOrWhiteSpace(Region.Alpha3Code)) {
            sb.Append('-').Append(Region.Alpha3Code);
          } else if (!string.IsNullOrWhiteSpace(Region.M49Code)) {
            sb.Append('-').Append(Region.M49Code);
          }
        }
      }
    }
  }

}

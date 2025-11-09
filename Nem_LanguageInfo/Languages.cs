using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo;

/// <summary>
/// Provides access to ISO 639 language information and lookup methods for language codes and names.
/// </summary>
public class Languages {
  private const string ISO_639_RESOURCE = "Nem_LanguageInfo.Data.iso-639-3.json";
  private readonly Language _langDefault;
  private readonly JsonSerializerOptions _serializerOptions = new() {
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  private static readonly Lazy<Languages> _lazyInstance = new(() => new Languages());

  /// <summary>
  /// Gets the singleton instance of the <see cref="Languages"/> class.
  /// </summary>
  public static Languages Instance { get => _lazyInstance.Value; }

  private readonly Dictionary<string, Language> _nameToLanguage = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, Language> _part1CodeToLanguage = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, Language> _part2BCodeToLanguage = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, Language> _part2TCodeToLanguage = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, Language> _part3CodeToLanguage = new(StringComparer.OrdinalIgnoreCase);

  private Languages() {
    Assembly assembly = Assembly.GetExecutingAssembly();

    using Stream stream = assembly.GetManifestResourceStream(ISO_639_RESOURCE)
    ?? throw new InvalidOperationException($"Resource '{ISO_639_RESOURCE}' not found.");

    List<Language> iso639Data = JsonSerializer.Deserialize<List<Language>>(stream, _serializerOptions)
    ?? throw new InvalidOperationException("Failed to deserialize language data.");

    foreach (Language iso639Model in iso639Data) {
      _nameToLanguage[iso639Model.Name] = iso639Model;
      foreach (string alias in iso639Model.Aliases) {
        if (!string.IsNullOrWhiteSpace(alias)) {
          _nameToLanguage[alias] = iso639Model;
        }
      }

      if (!string.IsNullOrWhiteSpace(iso639Model.Part1Code)) {
        _part1CodeToLanguage[iso639Model.Part1Code] = iso639Model;
      }
      if (!string.IsNullOrWhiteSpace(iso639Model.Part2BCode)) {
        _part2BCodeToLanguage[iso639Model.Part2BCode] = iso639Model;
      }
      if (!string.IsNullOrWhiteSpace(iso639Model.Part2TCode)) {
        _part2TCodeToLanguage[iso639Model.Part2TCode] = iso639Model;
      }
      if (!string.IsNullOrWhiteSpace(iso639Model.Part3Code)) {
        _part3CodeToLanguage[iso639Model.Part3Code] = iso639Model;
      }
    }

    _langDefault = _part3CodeToLanguage.GetValueOrDefault("und")
   ?? throw new InvalidOperationException("Default language 'und' not found in language data.");
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified language name or alias.
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="languageName">The name or alias of the language to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetFromName(string languageName) {
    if (string.IsNullOrWhiteSpace(languageName)) {
      return _langDefault;
    }
    return _nameToLanguage.GetValueOrDefault(languageName) ?? _langDefault;
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified ISO 639-1 (two-letter) code.
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="code">The ISO 639-1 code of the language to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetFromPart1Code(string code) {
    if (string.IsNullOrWhiteSpace(code)) {
      return _langDefault;
    }
    return _part1CodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified ISO 639-2/B (bibliographic) code.
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="code">The ISO 639-2/B code of the language to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetFromPart2BCode(string code) {
    if (string.IsNullOrWhiteSpace(code)) {
      return _langDefault;
    }
    return _part2BCodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified ISO 639-2/T (terminologic) code.
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="code">The ISO 639-2/T code of the language to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetFromPart2TCode(string code) {
    if (string.IsNullOrWhiteSpace(code)) {
      return _langDefault;
    }
    return _part2TCodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified ISO 639-3 (three-letter) code.
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="code">The ISO 639-3 code of the language to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetFromPart3Code(string code) {
    if (string.IsNullOrWhiteSpace(code)) {
      return _langDefault;
    }
    return _part3CodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  /// <summary>
  /// Gets the <see cref="Language"/> object for the specified language value.
  /// The value can be a language name, alias, or ISO 639 code (part 1, part 2B, part 2T, or part 3).
  /// Returns the default language if not found.
  /// </summary>
  /// <param name="languageValue">The language name, alias, or code to look up.</param>
  /// <returns>The corresponding <see cref="Language"/> object, or the default if not found.</returns>
  public Language GetLanguage(string languageValue) {
    if (string.IsNullOrWhiteSpace(languageValue)) {
      return _langDefault;
    }

    Language language;
    if (languageValue.Length == 2) {
      language = GetFromPart1Code(languageValue);
    } else if (languageValue.Length == 3) {
      language = GetFromPart3Code(languageValue);
      if (language == _langDefault) {
        language = GetFromPart2BCode(languageValue);
        if (language == _langDefault) {
          language = GetFromPart2TCode(languageValue);
        }
      }
    } else {
      language = GetFromName(languageValue);
    }

    return language;
  }

}
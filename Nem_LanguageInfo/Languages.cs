using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo;

public class Languages {
  private const string ISO_639_RESOURCE = "Nem_LanguageInfo.Data.iso-639-3.json";
  private readonly Language _langDefault;
  private readonly JsonSerializerOptions _serializerOptions = new() {
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };

  private static readonly Lazy<Languages> _lazyInstance = new(() => new Languages());
  public static Languages Instance { get => _lazyInstance.Value; }

  private readonly Dictionary<string, Language> _nameToLanguage = [];
  private readonly Dictionary<string, Language> _part1CodeToLanguage = [];
  private readonly Dictionary<string, Language> _part2BCodeToLanguage = [];
  private readonly Dictionary<string, Language> _part2TCodeToLanguage = [];
  private readonly Dictionary<string, Language> _part3CodeToLanguage = [];

  private Languages() {
    Assembly assembly = Assembly.GetExecutingAssembly();

    using Stream stream = assembly.GetManifestResourceStream(ISO_639_RESOURCE);
    List<Language> iso639Data = JsonSerializer.Deserialize<List<Language>>(stream, _serializerOptions);

    foreach (Language iso639Model in iso639Data) {
      _nameToLanguage[iso639Model.Name] = iso639Model;
      foreach (string alias in iso639Model.Aliases) {
        _nameToLanguage[alias] = iso639Model;
      }
      _part1CodeToLanguage[iso639Model.Part1Code] = iso639Model;
      _part2BCodeToLanguage[iso639Model.Part2BCode] = iso639Model;
      _part2TCodeToLanguage[iso639Model.Part2TCode] = iso639Model;
      _part3CodeToLanguage[iso639Model.Part3Code] = iso639Model;
    }

    _langDefault = _part3CodeToLanguage.GetValueOrDefault("und");
  }

  public Language GetFromName(string languageName) {
    Language langDefault = _part3CodeToLanguage.GetValueOrDefault("und");
    return _nameToLanguage.GetValueOrDefault(languageName) ?? langDefault;
  }

  public Language GetFromPart1Code(string code) {
    return _part1CodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  public Language GetFromPart2BCode(string code) {
    return _part2BCodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  public Language GetFromPart2TCode(string code) {
    return _part2TCodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  public Language GetFromPart3Code(string code) {
    return _part3CodeToLanguage.GetValueOrDefault(code) ?? _langDefault;
  }

  public Language GetLanguage(string languageValue) {
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
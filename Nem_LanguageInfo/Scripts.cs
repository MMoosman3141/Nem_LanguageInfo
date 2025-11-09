using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo;

public class Scripts {
  private const string ISO_15924_RESOURCE = "Nem_LanguageInfo.Data.iso-15924.json";
  private readonly JsonSerializerOptions _serializerOptions = new() {
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  private readonly Script _scriptDefault;

  private readonly Dictionary<string, Script> _scriptsByCode = [];
  private readonly Dictionary<string, Script> _scriptsByName = [];
  private readonly Dictionary<int, Script> _scriptsByNumber = [];

  private static readonly Lazy<Scripts> _lazyInstance = new(() => new Scripts());
  public static Scripts Instance { get => _lazyInstance.Value; }

  private Scripts() {
    Assembly assembly = Assembly.GetExecutingAssembly();

    using Stream stream = assembly.GetManifestResourceStream(ISO_15924_RESOURCE);
    List<Script> data = JsonSerializer.Deserialize<List<Script>>(stream, _serializerOptions);

    foreach (Script script in data) {
      _scriptsByCode[script.Code] = script;
      _scriptsByName[script.Name] = script;
      if (!string.IsNullOrWhiteSpace(script.Alias)) {
        _scriptsByName.TryAdd(script.Alias, script);
      }
      _scriptsByNumber[script.Number] = script;
    }

    _scriptDefault = _scriptsByCode.GetValueOrDefault("Zzzz");
  }

  public Script GetFromCode(string code) {
    return _scriptsByCode.GetValueOrDefault(code) ?? _scriptDefault;
  }

  public Script GetFromName(string name) {
    return _scriptsByName.GetValueOrDefault(name) ?? _scriptDefault;
  }

  public Script GetFromNumber(int number) {
    return _scriptsByNumber.GetValueOrDefault(number) ?? _scriptDefault;
  }

}

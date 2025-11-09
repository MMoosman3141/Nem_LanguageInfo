using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo;

/// <summary>
/// Provides access to ISO 15924 script information, including lookup by code, name, or number.
/// </summary>
public class Scripts {
  private const string ISO_15924_RESOURCE = "Nem_LanguageInfo.Data.iso-15924.json";
  private readonly Script _scriptDefault;
  private readonly JsonSerializerOptions _serializerOptions = new() {
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  private static readonly Lazy<Scripts> _lazyInstance = new(() => new Scripts());

  /// <summary>
  /// Gets the singleton instance of the <see cref="Scripts"/> class.
  /// </summary>
  public static Scripts Instance { get => _lazyInstance.Value; }

  private readonly Dictionary<string, Script> _scriptsByCode = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, Script> _scriptsByName = new(StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<int, Script> _scriptsByNumber = [];

  private Scripts() {
    Assembly assembly = Assembly.GetExecutingAssembly();

    using Stream stream = assembly.GetManifestResourceStream(ISO_15924_RESOURCE)
    ?? throw new InvalidOperationException($"Resource '{ISO_15924_RESOURCE}' not found.");

    List<Script> data = JsonSerializer.Deserialize<List<Script>>(stream, _serializerOptions)
    ?? throw new InvalidOperationException("Failed to deserialize script data.");

    foreach (Script script in data) {
      _scriptsByCode[script.Code] = script;
      _scriptsByName[script.Name] = script;
      if (!string.IsNullOrWhiteSpace(script.Alias)) {
        _scriptsByName.TryAdd(script.Alias, script);
      }
      _scriptsByNumber[script.Number] = script;
    }

    _scriptDefault = _scriptsByCode.GetValueOrDefault("Zzzz")
    ?? throw new InvalidOperationException("Default script 'Zzzz' not found in script data.");
  }

  /// <summary>
  /// Gets the <see cref="Script"/> instance for the specified ISO 15924 code.
  /// Returns a default script if the code is not found.
  /// </summary>
  /// <param name="code">The ISO 15924 script code to look up.</param>
  /// <returns>The matching <see cref="Script"/> or the default script.</returns>
  public Script GetFromCode(string code) {
    if (string.IsNullOrWhiteSpace(code)) {
      return _scriptDefault;
    }
    return _scriptsByCode.GetValueOrDefault(code) ?? _scriptDefault;
  }

  /// <summary>
  /// Gets the <see cref="Script"/> instance for the specified ISO 15924 script name.
  /// Returns a default script if the name is not found.
  /// </summary>
  /// <param name="name">The ISO 15924 script name to look up.</param>
  /// <returns>The matching <see cref="Script"/> or the default script.</returns>
  public Script GetFromName(string name) {
    if (string.IsNullOrWhiteSpace(name)) {
      return _scriptDefault;
    }
    return _scriptsByName.GetValueOrDefault(name) ?? _scriptDefault;
  }

  /// <summary>
  /// Gets the <see cref="Script"/> instance for the specified ISO 15924 script number.
  /// Returns a default script if the number is not found.
  /// </summary>
  /// <param name="number">The ISO 15924 script number to look up.</param>
  /// <returns>The matching <see cref="Script"/> or the default script.</returns>
  public Script GetFromNumber(int number) {
    return _scriptsByNumber.GetValueOrDefault(number) ?? _scriptDefault;
  }

}

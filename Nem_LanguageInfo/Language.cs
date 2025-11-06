using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  public class Language {
    public string Name { get; internal set; }
    public string Part1Code { get; internal set; }
    public string Part2TCode { get; internal set; }
    public string Part2BCode { get; internal set; }
    public string Part3Code { get; internal set; }
    public string Scope { get; internal set; }
    public string Type { get; internal set; }
    public string Comment { get; internal set; }
    public string DefaultScript { get; internal set; }
    public List<string> Aliases { get; internal set; } = [];

    [JsonIgnore]
    public Script Script { get => Scripts.Instance.GetFromCode(DefaultScript); }

  }
}

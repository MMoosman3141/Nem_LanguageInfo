using Nem_HierarchyTree;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo {
  public class Regions {
    private const string UM_M49_RESOURCE = "Nem_LanguageInfo.Data.un-m49.json";
    private const string ISO_3166_RESOURCE = "Nem_LanguageInfo.Data.ISO_3166.json";

    private readonly JsonSerializerOptions _serializerOptions = new() {
      WriteIndented = true,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly Dictionary<string, Area> _countriesByAlpha2 = [];
    private readonly Dictionary<string, Area> _countriesByAlpha3 = [];
    private readonly Dictionary<string, Area> _areaByName = [];
    private readonly Dictionary<string, Area> _areaByM49 = [];
    HierarchyTree<Area> _areaTree = new();

    private static readonly Lazy<Regions> _lazyInstance = new(() => new Regions());
    public static Regions Instance { get => _lazyInstance.Value; }

    private Regions() {
      Assembly assembly = Assembly.GetExecutingAssembly();

      using Stream countryStream = assembly.GetManifestResourceStream(ISO_3166_RESOURCE);
      List<Country> countryData = JsonSerializer.Deserialize<List<Country>>(countryStream, _serializerOptions);

      foreach (Country country in countryData) {
        Area area = new() {
          Name = country.Name,
          Alpha2Code = country.Alpha2,
          Alpha3Code = country.Alpha3,
          DefaultLanguageCode = country.PrimaryLanguage
        };
        _countriesByAlpha2[country.Alpha2] = area;
        _countriesByAlpha3[country.Alpha3] = area;
        _areaByName[country.Name] = area;
      }

      using Stream m49Stream = assembly.GetManifestResourceStream(ISO_3166_RESOURCE);
      List<Region> regionData = JsonSerializer.Deserialize<List<Region>>(m49Stream, _serializerOptions);

      Node<Area> parent = null;
      HashSet<Node<Area>> added = [];
      foreach (Region region in regionData) {
        AddArea(region.GlobalCode, region.GlobalName, added, ref parent);
        AddArea(region.RegionCode, region.RegionName, added, ref parent);
        AddArea(region.SubregionCode, region.SubregionName, added, ref parent);
        AddArea(region.IntermediateRegionCode, region.IntermediateRegionName, added, ref parent);
        //ToDo: Makes usre the added area is the same as we have in the counties dictionaries
        AddArea(region.M49Code, region.CountryOrArea, added, ref parent);

        Area country = _countriesByAlpha3.GetValueOrDefault(region.IsoAlpha3Code);
        if (country is not null) {
          country.M49Code = region.M49Code;
        }
      }
    }

    private void AddArea(string code, string name, HashSet<Node<Area>> added, ref Node<Area> parent) {
      if (!string.IsNullOrWhiteSpace(code)) {
        Area area = new() {
          M49Code = code,
          Name = name
        };
        if (_areaByM49.TryAdd(area.M49Code, area)) {
          added.TryGetValue(parent, out parent);

          Node<Area> regionNode = new(area);
          if (parent is not null) {
            regionNode.ParentId = parent.Id;
          }
          if (_areaTree.TryAdd(regionNode, out parent)) {
            added.Add(parent);
          }
        }
      }
    }
  }
}

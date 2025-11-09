using Nem_HierarchyTree;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Nem_LanguageInfo;

/// <summary>
/// Provides access to region and country information based on ISO 3166 and UN M49 standards.
/// Supports lookup by country codes, region codes, and names.
/// </summary>
public class Regions {
  private const string UM_M49_RESOURCE = "Nem_LanguageInfo.Data.un-m49.json";
  private const string ISO_3166_RESOURCE = "Nem_LanguageInfo.Data.ISO_3166.json";

  private readonly JsonSerializerOptions _serializerOptions = new() {
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };

  private readonly Dictionary<string, Area> _countriesByAlpha2 = [];
  private readonly Dictionary<string, Area> _countriesByAlpha3 = [];
  private readonly Dictionary<string, Area> _areaByName = [];
  private readonly Dictionary<string, Area> _areaByM49 = [];
  private readonly HierarchyTree<Area> _areaTree = [];

  private static readonly Lazy<Regions> _lazyInstance = new(() => new Regions());

  /// <summary>
  /// Gets the singleton instance of the <see cref="Regions"/> class.
  /// </summary>
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

    using Stream m49Stream = assembly.GetManifestResourceStream(UM_M49_RESOURCE);
    List<Region> regionData = JsonSerializer.Deserialize<List<Region>>(m49Stream, _serializerOptions);

    Node<Area> parent = null;
    HashSet<Node<Area>> added = [];
    foreach (Region region in regionData) {
      AddArea(region.GlobalCode, region.GlobalName, added, ref parent);
      AddArea(region.RegionCode, region.RegionName, added, ref parent);
      AddArea(region.SubregionCode, region.SubregionName, added, ref parent);
      AddArea(region.IntermediateRegionCode, region.IntermediateRegionName, added, ref parent);
      AddCountry(region.M49Code, region.IsoAlpha3Code, added, ref parent);
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

  private void AddCountry(string code, string iso3166Code, HashSet<Node<Area>> added, ref Node<Area> parent) {
    if (!string.IsNullOrWhiteSpace(code)) {
      if (iso3166Code is null || !_countriesByAlpha3.TryGetValue(iso3166Code, out Area area)) {
        area = new();
      }
      area.M49Code = code;

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

  /// <summary>
  /// Gets the <see cref="Area"/> information for a region or country using its name.
  /// </summary>
  /// <param name="reaName">The name of the region or country.</param>
  /// <returns>The <see cref="Area"/> corresponding to the specified name, or null if not found.</returns>
  public Area GetAReaFromName(string reaName) {
    return _areaByName.GetValueOrDefault(reaName);
  }

  /// <summary>
  /// Gets the <see cref="Area"/> information for a country using its ISO 3166-1 alpha-2 code.
  /// </summary>
  /// <param name="alpha2Code">The ISO 3166-1 alpha-2 country code.</param>
  /// <returns>The <see cref="Area"/> corresponding to the specified alpha-2 code, or null if not found.</returns>
  public Area GetAreaFromIso3166Alpha2Code(string alpha2Code) {
    return _countriesByAlpha2.GetValueOrDefault(alpha2Code);
  }

  /// <summary>
  /// Gets the <see cref="Area"/> information for a country using its ISO 3166-1 alpha-3 code.
  /// </summary>
  /// <param name="alpha3Code">The ISO 3166-1 alpha-3 country code.</param>
  /// <returns>The <see cref="Area"/> corresponding to the specified alpha-3 code, or null if not found.</returns>
  public Area GetAreaFromIso3166Alpha3Code(string alpha3Code) {
    return _countriesByAlpha3.GetValueOrDefault(alpha3Code);
  }

  /// <summary>
  /// Gets the <see cref="Area"/> information for a region or country using its UN M49 code.
  /// </summary>
  /// <param name="m49Code">The UN M49 code for the region or country.</param>
  /// <returns>The <see cref="Area"/> corresponding to the specified M49 code, or null if not found.</returns>
  public Area GetAreaFromUnM49Code(string m49Code) {
    return _areaByM49.GetValueOrDefault(m49Code);
  }
}

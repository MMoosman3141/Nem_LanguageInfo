using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  internal class Region {
    public string GlobalCode { get; private set; }
    public string GlobalName { get; private set; }

    public string RegionCode { get; private set; }
    public string RegionName { get; private set; }

    public string SubregionCode { get; private set; }
    public string SubregionName { get; private set; }

    public string IntermediateRegionCode { get; private set; }
    public string IntermediateRegionName { get; private set; }

    public string CountryOrArea { get; private set; }
    public string M49Code { get; private set; }

    public string IsoAlpha3Code { get; private set; }

    [JsonConstructor]
    public Region(string globalCode, string globalName, string regionCode, string regionName,
      string subregionCode, string subregionName, string intermediateRegionCode, string intermediateRegionName,
      string countryOrArea, string m49Code, string isoAlpha3Code) {
      GlobalCode = globalCode;
      GlobalName = globalName;
      RegionCode = regionCode;
      RegionName = regionName;
      SubregionCode = subregionCode;
      SubregionName = subregionName;
      IntermediateRegionCode = intermediateRegionCode;
      IntermediateRegionName = intermediateRegionName;
      CountryOrArea = countryOrArea;
      M49Code = m49Code;
      IsoAlpha3Code = isoAlpha3Code;
    }
  }
}

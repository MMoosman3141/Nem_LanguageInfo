using System.Text.Json.Serialization;

namespace Nem_LanguageInfo {
  [method: JsonConstructor]
  internal class Region(string globalCode, string globalName, string regionCode, string regionName,
    string subregionCode, string subregionName, string intermediateRegionCode, string intermediateRegionName,
    string countryOrArea, string m49Code, string isoAlpha3Code) {
    public string GlobalCode { get; private set; } = globalCode;
    public string GlobalName { get; private set; } = globalName;

    public string RegionCode { get; private set; } = regionCode;
    public string RegionName { get; private set; } = regionName;

    public string SubregionCode { get; private set; } = subregionCode;
    public string SubregionName { get; private set; } = subregionName;

    public string IntermediateRegionCode { get; private set; } = intermediateRegionCode;
    public string IntermediateRegionName { get; private set; } = intermediateRegionName;

    public string CountryOrArea { get; private set; } = countryOrArea;
    public string M49Code { get; private set; } = m49Code;

    public string IsoAlpha3Code { get; private set; } = isoAlpha3Code;
  }
}

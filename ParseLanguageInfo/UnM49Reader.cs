using ParseLanguageInfo.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ParseLanguageInfo {
  internal class UnM49Reader {
    private readonly JsonSerializerOptions _serializerOptions = new() {
      WriteIndented = true,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Reads the UN M.49 CSV file and writes the data to a JSON file
    /// </summary>
    /// <param name="csvFilePath">Path to the UN_M49.csv file</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void ConvertCsvToJson(string csvFilePath, string jsonOutputPath) {
      string outputDir = Path.GetDirectoryName(jsonOutputPath);
      if (!Directory.Exists(outputDir)) {
        Directory.CreateDirectory(outputDir);
      }

      List<UnM49DataModel> countries = ReadCsv(csvFilePath);
      WriteJson(countries, jsonOutputPath);
    }

    /// <summary>
    /// Reads the UN M.49 CSV file and returns a list of country/region data models
    /// </summary>
    /// <param name="csvFilePath">Path to the UN_M49.csv file</param>
    /// <returns>List of UnM49DataModel objects</returns>
    public static List<UnM49DataModel> ReadCsv(string csvFilePath) {
      List<UnM49DataModel> countries = [];

      string[] lines = File.ReadAllLines(csvFilePath, Encoding.UTF8);

      // Skip header line
      for (int i = 1; i < lines.Length; i++) {
        string line = lines[i].Trim();
        if (string.IsNullOrWhiteSpace(line)) {
          continue;
        }

        string[] fields = line.Split(';');
        if (fields.Length < 15) {
          continue;
        }

        UnM49DataModel country = new() {
          GlobalCode = fields[0].Trim(),
          GlobalName = fields[1].Trim(),
          RegionCode = fields[2].Trim(),
          RegionName = fields[3].Trim(),
          SubregionCode = fields[4].Trim(),
          SubregionName = fields[5].Trim(),
          IntermediateRegionCode = fields[6].Trim(),
          IntermediateRegionName = fields[7].Trim(),
          CountryOrArea = fields[8].Trim(),
          M49Code = fields[9].Trim(),
          IsoAlpha2Code = fields[10].Trim(),
          IsoAlpha3Code = fields[11].Trim(),
          LeastDevelopedCountries = fields[12].Trim(),
          LandLockedDevelopingCountries = fields[13].Trim(),
          SmallIslandDevelopingStates = fields[14].Trim()
        };

        countries.Add(country);
      }

      return countries;
    }

    /// <summary>
    /// Writes the list of countries/regions to a JSON file
    /// </summary>
    /// <param name="countries">List of UnM49DataModel objects</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void WriteJson(List<UnM49DataModel> countries, string jsonOutputPath) {
      string json = JsonSerializer.Serialize(countries, _serializerOptions);

      // Ensure output directory exists
      string directory = Path.GetDirectoryName(jsonOutputPath);
      if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
        Directory.CreateDirectory(directory);
      }

      File.WriteAllText(jsonOutputPath, json, Encoding.UTF8);
    }
  }
}

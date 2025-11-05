using ParseLanguageInfo.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ParseLanguageInfo {
  internal class Iso15924Reader {
    readonly JsonSerializerOptions _serializerOptions = new() {
      WriteIndented = true,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Reads the ISO-15924 CSV file and writes the data to a JSON file
    /// </summary>
    /// <param name="csvFilePath">Path to the ISO-15924_Codes.csv file</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void ConvertCsvToJson(string csvFilePath, string jsonOutputPath) {
      string outputDir = Path.GetDirectoryName(jsonOutputPath);
      if (!Directory.Exists(outputDir)) {
        Directory.CreateDirectory(outputDir);
      }

      List<Iso15924DataModel> scripts = ReadCsv(csvFilePath);
      WriteJson(scripts, jsonOutputPath);
    }

    /// <summary>
    /// Reads the ISO-15924 CSV file and returns a list of script data models
    /// </summary>
    /// <param name="csvFilePath">Path to the ISO-15924_Codes.csv file</param>
    /// <returns>List of Iso15924DataModel objects</returns>
    public static List<Iso15924DataModel> ReadCsv(string csvFilePath) {
      List<Iso15924DataModel> scripts = [];

      string[] lines = File.ReadAllLines(csvFilePath, Encoding.UTF8);

      // Skip header line
      for (int i = 1; i < lines.Length; i++) {
        string line = lines[i].Trim();
        if (string.IsNullOrWhiteSpace(line)) {
          continue;
        }

        string[] fields = line.Split('\t');
        if (fields.Length < 7) {
          continue;
        }

        Iso15924DataModel script = new() {
          Code = fields[0].Trim(),
          Number = ParseInt(fields[1].Trim()),
          EnglishName = fields[2].Trim(),
          Alias = fields[4].Trim(),
          Age = ParseAge(fields[5].Trim()),
          Date = ParseDate(fields[6].Trim())
        };

        scripts.Add(script);
      }

      return scripts;
    }

    /// <summary>
    /// Writes the list of scripts to a JSON file
    /// </summary>
    /// <param name="scripts">List of Iso15924DataModel objects</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void WriteJson(List<Iso15924DataModel> scripts, string jsonOutputPath) {
      string json = JsonSerializer.Serialize(scripts, _serializerOptions);

      // Ensure output directory exists
      string directory = Path.GetDirectoryName(jsonOutputPath);
      if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
        Directory.CreateDirectory(directory);
      }

      File.WriteAllText(jsonOutputPath, json, Encoding.UTF8);
    }

    private static int ParseInt(string value) {
      if (int.TryParse(value, out int result)) {
        return result;
      }
      return 0;
    }

    private static int ParseAge(string value) {
      if (string.IsNullOrWhiteSpace(value)) {
        return 0;
      }

      // Age format is like "9.0" - extract the major version
      if (double.TryParse(value, out double age)) {
        return (int)age;
      }

      return 0;
    }

    private static DateTime ParseDate(string value) {
      if (DateTime.TryParse(value, out DateTime result)) {
        return result;
      }
      return DateTime.MinValue;
    }
  }
}

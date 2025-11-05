using ParseLanguageInfo.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ParseLanguageInfo {
  internal class Iso639Reader {
    private readonly JsonSerializerOptions _serializerOptions = new() {
      WriteIndented = true,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>
    /// Reads the ISO-639-3 CSV file and writes the data to a JSON file
    /// </summary>
    /// <param name="csvFilePath">Path to the iso-639-3.csv file</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void ConvertCsvToJson(string csvFilePath, string jsonOutputPath) {
      string outputDir = Path.GetDirectoryName(jsonOutputPath);
      if (!Directory.Exists(outputDir)) {
        Directory.CreateDirectory(outputDir);
      }

      List<Iso639Data> languages = ReadCsv(csvFilePath);
      WriteJson(languages, jsonOutputPath);
    }

    /// <summary>
    /// Reads the ISO-639-3 CSV file and returns a list of language data models
    /// </summary>
    /// <param name="csvFilePath">Path to the iso-639-3.csv file</param>
    /// <returns>List of Iso639Data objects</returns>
    public static List<Iso639Data> ReadCsv(string csvFilePath) {
      List<Iso639Data> languages = [];

      string[] lines = File.ReadAllLines(csvFilePath, Encoding.UTF8);

      // Skip header line
      for (int i = 1; i < lines.Length; i++) {
        string line = lines[i].Trim();
        if (string.IsNullOrWhiteSpace(line)) {
          continue;
        }

        string[] fields = line.Split('\t', StringSplitOptions.TrimEntries);

        Iso639Data language = new() {
          Part3Code = fields[0],
          Part2BCode = fields[1],
          Part2TCode = fields[2],
          Part1Code = fields[3],
          Scope = fields[4],
          Type = fields[5],
          Name = fields[6],
          Comment = fields.Length > 7 ? fields[7] : string.Empty,
          DefaultScript = LanguageScriptMapper.GetDefaultScript(fields[3], fields[0])
        };

        languages.Add(language);
      }

      return languages;
    }

    /// <summary>
    /// Writes the list of languages to a JSON file
    /// </summary>
    /// <param name="languages">List of Iso639Data objects</param>
    /// <param name="jsonOutputPath">Path where the JSON output should be saved</param>
    public void WriteJson(List<Iso639Data> languages, string jsonOutputPath) {
      string json = JsonSerializer.Serialize(languages, _serializerOptions);

      // Ensure output directory exists
      string directory = Path.GetDirectoryName(jsonOutputPath);
      if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
        Directory.CreateDirectory(directory);
      }

      File.WriteAllText(jsonOutputPath, json, Encoding.UTF8);
    }
  }
}

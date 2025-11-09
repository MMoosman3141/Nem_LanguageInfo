namespace Nem_LanguageInfo; 

/// <summary>
/// Represents a geographical area with associated codes and default language.
/// </summary>
public class Area {
  /// <summary>
  /// Gets or sets the UN M49 code for the area.
  /// </summary>
  public string M49Code { get; set; }

  /// <summary>
  /// Gets or sets the name of the area.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the ISO Alpha-2 code for the area.
  /// </summary>
  public string Alpha2Code { get; set; }

  /// <summary>
  /// Gets or sets the ISO Alpha-3 code for the area.
  /// </summary>
  public string Alpha3Code { get; set; }

  /// <summary>
  /// Gets or sets the default language code for the area.
  /// </summary>
  public string DefaultLanguageCode { get; set; }
}

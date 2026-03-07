namespace Nem_LanguageInfo;

/// <summary>
/// Specifies options for language dialect selection and formatting.
/// </summary>
public enum DialectOptions {
#pragma warning disable CS1591
  None = 0b0,
  LanguagePreferPart3 = 0b1,
  Script = 0b10,
  Region = 0b100,
  RegionPreferUnM49 = 0b1100, // Takes precedence over RegionPreferAlpha3
  RegionPreferAlpha3 = 0b10100,
  [Obsolete("Use RegionPreferAlpha3 instead.")]
  RegionPeferAlpha3 = 0b10100,
  Variant = 0b100000
#pragma warning restore CS1591
}

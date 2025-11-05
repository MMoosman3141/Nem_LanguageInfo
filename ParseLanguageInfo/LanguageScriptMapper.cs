using System.Collections.Generic;

namespace ParseLanguageInfo {
  /// <summary>
  /// Provides mapping between ISO 639 language codes and their default ISO 15924 script codes
  /// </summary>
  internal class LanguageScriptMapper {
    private static readonly Dictionary<string, string> _languageToScriptMap = new() {
      // Common languages with their primary scripts
 { "en", "Latn" },  // English -> Latin
      { "es", "Latn" },  // Spanish -> Latin
      { "fr", "Latn" },  // French -> Latin
      { "de", "Latn" },  // German -> Latin
      { "it", "Latn" },  // Italian -> Latin
      { "pt", "Latn" },  // Portuguese -> Latin
      { "ru", "Cyrl" },  // Russian -> Cyrillic
      { "ar", "Arab" },  // Arabic -> Arabic
      { "zh", "Hani" },  // Chinese -> Han (can also be Hans or Hant)
      { "ja", "Jpan" },  // Japanese -> Japanese (Han + Hiragana + Katakana)
      { "ko", "Kore" },  // Korean -> Korean (Hangul + Han)
      { "hi", "Deva" },  // Hindi -> Devanagari
      { "bn", "Beng" },  // Bengali -> Bengali
      { "pa", "Guru" },  // Punjabi -> Gurmukhi
      { "te", "Telu" },  // Telugu -> Telugu
      { "ta", "Taml" },  // Tamil -> Tamil
      { "mr", "Deva" },  // Marathi -> Devanagari
      { "gu", "Gujr" },  // Gujarati -> Gujarati
      { "kn", "Knda" },  // Kannada -> Kannada
      { "ml", "Mlym" },  // Malayalam -> Malayalam
   { "or", "Orya" },  // Odia -> Oriya
   { "th", "Thai" },  // Thai -> Thai
 { "my", "Mymr" },  // Burmese -> Myanmar
      { "km", "Khmr" },  // Khmer -> Khmer
    { "lo", "Laoo" },  // Lao -> Lao
      { "si", "Sinh" },  // Sinhala -> Sinhala
      { "am", "Ethi" },  // Amharic -> Ethiopic
      { "el", "Grek" },  // Greek -> Greek
   { "he", "Hebr" },  // Hebrew -> Hebrew
      { "yi", "Hebr" },  // Yiddish -> Hebrew
      { "hy", "Armn" },// Armenian -> Armenian
      { "ka", "Geor" },  // Georgian -> Georgian
    { "bo", "Tibt" },  // Tibetan -> Tibetan
      { "ug", "Arab" },  // Uyghur -> Arabic
      { "dv", "Thaa" },  // Divehi -> Thaana
      { "chr", "Cher" }, // Cherokee -> Cherokee
      { "ii", "Yiii" },  // Sichuan Yi -> Yi
      { "vai", "Vaii" }, // Vai -> Vai
      { "hbs", "Latn" }, // Serbo-Croatian -> Latin (can also be Cyrl)
      { "sr", "Cyrl" },  // Serbian -> Cyrillic (can also be Latn)
      { "bs", "Latn" },  // Bosnian -> Latin
      { "hr", "Latn" },  // Croatian -> Latin
      { "uk", "Cyrl" },  // Ukrainian -> Cyrillic
      { "be", "Cyrl" },  // Belarusian -> Cyrillic
      { "bg", "Cyrl" },  // Bulgarian -> Cyrillic
      { "mk", "Cyrl" },  // Macedonian -> Cyrillic
      { "fa", "Arab" },  // Persian -> Arabic
      { "ps", "Arab" },  // Pashto -> Arabic
      { "ur", "Arab" },  // Urdu -> Arabic
    { "sd", "Arab" },  // Sindhi -> Arabic
  { "ku", "Arab" },  // Kurdish -> Arabic
      { "az", "Latn" },  // Azerbaijani -> Latin
      { "tr", "Latn" },  // Turkish -> Latin
   { "uz", "Latn" },  // Uzbek -> Latin
      { "kk", "Cyrl" },  // Kazakh -> Cyrillic
      { "ky", "Cyrl" },  // Kyrgyz -> Cyrillic
      { "tg", "Cyrl" },  // Tajik -> Cyrillic
      { "tk", "Latn" },  // Turkmen -> Latin
      { "mn", "Cyrl" },  // Mongolian -> Cyrillic
      { "ne", "Deva" },  // Nepali -> Devanagari
      { "sa", "Deva" },  // Sanskrit -> Devanagari
      { "ks", "Arab" },// Kashmiri -> Arabic
      { "vi", "Latn" },  // Vietnamese -> Latin
      { "jv", "Latn" },  // Javanese -> Latin (historically Java)
      { "su", "Latn" },  // Sundanese -> Latin
 { "ms", "Latn" },  // Malay -> Latin
      { "id", "Latn" },  // Indonesian -> Latin
      { "tl", "Latn" },  // Tagalog -> Latin
      { "fil", "Latn" }, // Filipino -> Latin
{ "sw", "Latn" },  // Swahili -> Latin
      { "ha", "Latn" },  // Hausa -> Latin
      { "yo", "Latn" },  // Yoruba -> Latin
      { "ig", "Latn" },  // Igbo -> Latin
      { "zu", "Latn" },  // Zulu -> Latin
      { "xh", "Latn" },  // Xhosa -> Latin
      { "af", "Latn" },  // Afrikaans -> Latin
      { "sq", "Latn" },  // Albanian -> Latin
      { "eu", "Latn" },  // Basque -> Latin
      { "ca", "Latn" },  // Catalan -> Latin
      { "gl", "Latn" },  // Galician -> Latin
      { "cy", "Latn" },  // Welsh -> Latin
      { "ga", "Latn" },  // Irish -> Latin
      { "gd", "Latn" },  // Scottish Gaelic -> Latin
   { "is", "Latn" },  // Icelandic -> Latin
      { "no", "Latn" },// Norwegian -> Latin
      { "nn", "Latn" },  // Norwegian Nynorsk -> Latin
      { "nb", "Latn" },  // Norwegian Bokmål -> Latin
      { "sv", "Latn" },  // Swedish -> Latin
      { "da", "Latn" },  // Danish -> Latin
{ "fi", "Latn" },  // Finnish -> Latin
      { "et", "Latn" },  // Estonian -> Latin
   { "lv", "Latn" },  // Latvian -> Latin
      { "lt", "Latn" },  // Lithuanian -> Latin
      { "pl", "Latn" },  // Polish -> Latin
   { "cs", "Latn" },  // Czech -> Latin
      { "sk", "Latn" },  // Slovak -> Latin
      { "sl", "Latn" },  // Slovenian -> Latin
      { "hu", "Latn" },  // Hungarian -> Latin
      { "ro", "Latn" },  // Romanian -> Latin
      { "nl", "Latn" },  // Dutch -> Latin
    };

    /// <summary>
 /// Gets the default ISO 15924 script code for a given language
    /// </summary>
    /// <param name="part1Code">ISO 639-1 code (2-letter)</param>
    /// <param name="part3Code">ISO 639-3 code (3-letter)</param>
    /// <returns>ISO 15924 script code, or "Zyyy" (undetermined) if no mapping exists</returns>
 public static string GetDefaultScript(string part1Code, string part3Code) {
      // Try ISO 639-1 code first (most common mappings use this)
      if (!string.IsNullOrEmpty(part1Code) && _languageToScriptMap.TryGetValue(part1Code, out string script)) {
        return script;
      }

      // Try ISO 639-3 code
      if (!string.IsNullOrEmpty(part3Code) && _languageToScriptMap.TryGetValue(part3Code, out script)) {
        return script;
    }

      // Default to "Zyyy" (Common/Undetermined script) if no mapping found
      return "Zyyy";
    }
  }
}

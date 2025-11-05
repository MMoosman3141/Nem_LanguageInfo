namespace ParseLanguageInfo {
  /// <summary>
  /// Maps ISO 639-3 language codes to their commonly known alternative names
  /// </summary>
  internal static class LanguageAlternativeNamesMapper {
    private static readonly Dictionary<string, List<string>> _alternativeNames = new() {
      // Major world languages
      
      { "spa", new List<string> { "Spanish Language", "Castilian" } },
      
      { "jpn", new List<string> { "Japanese Language", "Nippon-go" } },
      { "kor", new List<string> { "Korean Language", "Hangugeo" } },
  
      { "ben", new List<string> { "Bengali Language", "Bangla" } },
      { "zho", new List<string> { "Chinese Language", "Mandarin", "Putonghua" } },
  
      { "tha", new List<string> { "Thai Language", "Siamese" } },
  
      { "nld", new List<string> { "Dutch Language", "Flemish" } },
  
      { "fin", new List<string> { "Finnish Language", "Suomi" } },
      { "ell", new List<string> { "Greek Language", "Modern Greek", "Ellinika" } },
      { "heb", new List<string> { "Hebrew Language", "Ivrit" } },
  
      { "hun", new List<string> { "Hungarian Language", "Magyar" } },
  
      { "slv", new List<string> { "Slovenian Language", "Slovene" } },
      { "cat", new List<string> { "Catalan Language", "Valencian" } },
  
  { "lav", new List<string> { "Latvian Language", "Lettish" } },
  
      
      // Asian languages
      { "ind", new List<string> { "Indonesian Language", "Bahasa Indonesia" } },
      { "msa", new List<string> { "Malay Language", "Bahasa Melayu" } },
      { "tgl", new List<string> { "Tagalog Language", "Filipino" } },
 
      { "pan", new List<string> { "Punjabi Language", "Panjabi" } },
 
      { "ori", new List<string> { "Oriya Language", "Odia" } },
      { "mya", new List<string> { "Burmese Language", "Myanmar Language" } },
      { "khm", new List<string> { "Khmer Language", "Cambodian" } },
      { "lao", new List<string> { "Lao Language", "Laotian" } },
      { "sin", new List<string> { "Sinhala Language", "Sinhalese" } },
 
      { "kat", new List<string> { "Georgian Language", "Kartuli" } },
    { "hye", new List<string> { "Armenian Language", "Hayeren" } },
      { "fas", new List<string> { "Persian Language", "Farsi" } },
      { "pus", new List<string> { "Pashto Language", "Pushto" } },
 
      { "azj", new List<string> { "Azerbaijani Language", "Azeri" } },
      { "aze", new List<string> { "Azerbaijani Language", "Azeri" } },
 
      { "kir", new List<string> { "Kyrgyz Language", "Kirghiz" } },
      
      // African languages
      { "swa", new List<string> { "Swahili Language", "Kiswahili" } },
 
      { "zul", new List<string> { "Zulu Language", "IsiZulu" } },
      { "xho", new List<string> { "Xhosa Language", "IsiXhosa" } },
 
      
      // European minority languages
    { "eus", new List<string> { "Basque Language", "Euskara" } },
      { "gle", new List<string> { "Irish Language", "Gaeilge" } },
      { "gla", new List<string> { "Scottish Gaelic", "Gàidhlig" } },
      { "cym", new List<string> { "Welsh Language", "Cymraeg" } },
 
      { "ltz", new List<string> { "Luxembourgish Language", "Letzeburgesch" } },
 
      { "sqi", new List<string> { "Albanian Language", "Shqip" } },
      
      // Native American languages
      { "nav", new List<string> { "Navajo Language", "Navaho", "Diné bizaad" } },
 
      
      // Ancient/Historical languages
      { "lat", new List<string> { "Latin Language", "Classical Latin" } },
      { "grc", new List<string> { "Ancient Greek", "Classical Greek" } },
 
   { "chu", new List<string> { "Church Slavonic", "Old Church Slavonic" } },
 
      { "syc", new List<string> { "Syriac Language", "Classical Syriac" } },
      
      // Sign languages
 
    { "fsl", new List<string> { "FSL", "French Sign" } },
      
   // Regional variants
      { "arb", new List<string> { "Modern Standard Arabic", "MSA" } },
      { "cmn", new List<string> { "Mandarin Chinese", "Standard Chinese", "Guoyu" } },
      { "yue", new List<string> { "Cantonese", "Yue Chinese" } },
      { "wuu", new List<string> { "Wu Chinese", "Shanghainese" } },
   { "nan", new List<string> { "Min Nan", "Hokkien", "Taiwanese" } },
      
      // Celtic languages
      { "cor", new List<string> { "Cornish Language", "Kernowek" } },
      { "glv", new List<string> { "Manx Language", "Gaelg" } },
    };

    /// <summary>
    /// Gets alternative names for a given language code
    /// </summary>
    /// <param name="part3Code">ISO 639-3 language code</param>
    /// <returns>List of alternative names, or empty list if none found</returns>
    public static List<string> GetAlternativeNames(string part3Code) {
    if (string.IsNullOrWhiteSpace(part3Code)) {
  return new List<string>();
      }

      return _alternativeNames.TryGetValue(part3Code.ToLowerInvariant(), out var names) 
        ? new List<string>(names) 
  : new List<string>();
    }

    /// <summary>
    /// Checks if a language has alternative names
    /// </summary>
    /// <param name="part3Code">ISO 639-3 language code</param>
    /// <returns>True if alternative names exist, false otherwise</returns>
    public static bool HasAlternativeNames(string part3Code) {
      return !string.IsNullOrWhiteSpace(part3Code) && 
      _alternativeNames.ContainsKey(part3Code.ToLowerInvariant());
    }
  }
}

using Nem_LanguageInfo;
using System.Reflection;

namespace xUnit_Nem_LanguageInfo;

public class DialectTests {
  [Fact]
  public void Constructor_WithLanguageOnly_ShouldCreateDialect() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");

    // Act
    Dialect dialect = new(english);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal(english, dialect.Language);
    Assert.Null(dialect.Region);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageAndRegion_ShouldCreateDialect() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");
    Area usa = Regions.Instance.GetArea("US");

    // Act
    Dialect dialect = new(english, usa);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal(english, dialect.Language);
    Assert.Equal(usa, dialect.Region);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageRegionAndScript_ShouldCreateDialect() {
    // Arrange
    Language chinese = Languages.Instance.GetLanguage("zh");
    Area china = Regions.Instance.GetArea("CN");
    Script hans = Scripts.Instance.GetFromCode("Hans");

    // Act
    Dialect dialect = new(chinese, china, hans);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal(chinese, dialect.Language);
    Assert.Equal(china, dialect.Region);
    Assert.Equal(hans, dialect.Script);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithAllParameters_ShouldCreateDialect() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");
    Area usa = Regions.Instance.GetArea("US");
    Script latn = Scripts.Instance.GetFromCode("Latn");
    string variant = "posix";

    // Act
    Dialect dialect = new(english, usa, latn, variant);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal(english, dialect.Language);
    Assert.Equal(usa, dialect.Region);
    Assert.Equal(latn, dialect.Script);
    Assert.Equal(variant, dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Null(dialect.Region);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageAndRegionCodes_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageRegionAndScriptCodes_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("zh", "CN", "Hans");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("zh", dialect.Language.Part1Code);
    Assert.Equal("CN", dialect.Region.Alpha2Code);
    Assert.Equal("Hans", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_WithAllCodeParameters_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Equal("posix", dialect.Variant);
  }

  [Fact]
  public void Constructor_WithInvalidRegionCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.FromParts("en", "ZZ"));
  }

  [Fact]
  public void Constructor_WithInvalidLanguageCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.FromParts("zzz", "US"));
  }

  [Fact]
  public void Constructor_WithInvalidScriptCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.FromParts("en", "US", "Xxxx"));
  }

  [Fact]
  public void Parse_WithValidBcp47_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("zh-Hans-CN");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("zh", dialect.Language.Part1Code);
    Assert.Equal("Hans", dialect.Script.Code);
    Assert.Equal("CN", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void Parse_WithInvalidBcp47_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.Parse("not-a-valid-bcp47"));
  }

  [Fact]
  public void FromParts_WithValidCodes_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Equal("posix", dialect.Variant);
  }

  [Fact]
  public void TryParse_WithValidBcp47_ShouldReturnTrueAndDialect() {
    // Arrange & Act
    bool success = Dialect.TryParse("en-US", out Dialect dialect);

    // Assert
    Assert.True(success);
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void TryParse_WithInvalidBcp47_ShouldReturnFalseAndNullDialect() {
    // Arrange & Act
    bool success = Dialect.TryParse("not-a-valid-bcp47", out Dialect dialect);

    // Assert
    Assert.False(success);
    Assert.Null(dialect);
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public void Constructor_WithMissingLanguageCode_ShouldThrowArgumentException(string languageCode) {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.FromParts(languageCode: languageCode, regionCode: "US"));
  }

  [Fact]
  public void Constructor_WithNullLanguageObject_ShouldThrowArgumentNullException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentNullException>(() => new Dialect((Language)null));
  }

  [Fact]
  public void Constructor_WithM49RegionCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "840");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("840", dialect.Region.M49Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageOnly_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Null(dialect.Region);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageAndRegion_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-US");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageScriptAndRegion_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("zh-Hans-CN");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("zh", dialect.Language.Part1Code);
    Assert.Equal("Hans", dialect.Script.Code);
    Assert.Equal("CN", dialect.Region.Alpha2Code);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithAllComponents_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-Latn-US-posix");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
    Assert.Equal("posix", dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageAndScript_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("zh-Hans");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("zh", dialect.Language.Part1Code);
    Assert.Equal("Hans", dialect.Script.Code);
    Assert.Null(dialect.Region);
    Assert.Null(dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageAndVariant_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-posix");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Region);
    Assert.Equal("posix", dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithThreeLetterLanguageCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("eng-US");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("eng", dialect.Language.Part3Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithM49RegionCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-840");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("840", dialect.Region.M49Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithInvalidFormat_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.Parse("not-a-valid-bcp47"));
  }

  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  public void Constructor_FromBcp47_WithMissingInput_ShouldThrowArgumentException(string bcp47Code) {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.Parse(bcp47Code));
  }

  [Fact]
  public void Constructor_FromBcp47_WithInvalidRegionCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.Parse("en-ZZ"));
  }

  [Fact]
  public void Constructor_FromBcp47_WithInvalidScriptCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => Dialect.Parse("en-Xxxx-US"));
  }

  [Fact]
  public void Constructor_FromBcp47_CaseInsensitive_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("EN-us");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void ToString_WithLanguageOnly_ShouldReturnLanguageCode() {
    string expected = "en";
    
    // Arrange
    Dialect dialect = Dialect.FromParts("en");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithLanguageAndRegion_ShouldReturnLanguageOnlyByDefault() {
    string expected = "en";

    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithLanguageScriptAndRegion_ShouldReturnLanguageOnlyByDefault() {
    string expected = "zh";
    
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", "CN", "Hans");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithAllComponents_ShouldReturnLanguageOnlyByDefault() {
    string expected = "en";

    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithLanguageAndScript_ShouldReturnLanguageOnlyByDefault() {
    string expected = "zh";
    
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", null, "Hans");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithLanguageAndVariant_ShouldReturnLanguageOnlyByDefault() {
    string expected = "en";
    
    // Arrange
    Dialect dialect = Dialect.FromParts("en", null, null, "posix");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("eng", "en")]
  [InlineData("cmn", "cmn")]
  [InlineData("yue", "yue")]
  public void ToString_WithThreeLetterLanguageCode_ShouldUseThreeLetterCode(string part3Code, string expected) {
    // Arrange
    Language language = Languages.Instance.GetFromPart3Code(part3Code);
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("en-US", "en")]
  [InlineData("zh-Hans-CN", "zh")]
  [InlineData("fr-CA", "fr")]
  [InlineData("de-DE", "de")]
  public void ToString_WithVariousBcp47Codes_ShouldReturnCorrectFormat(string bcp47, string expected) {
    // Arrange
    Dialect dialect = Dialect.Parse(bcp47);

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void RoundTrip_FromBcp47ToString_WithDefaultToString_ShouldReturnLanguageOnly() {
    // Arrange
    string original = "en-Latn-US-posix";
    string expected = "en";
    Dialect dialect = Dialect.Parse(original);

    // Act
    string actual = dialect.ToString();

    // Assert
    Assert.Equal(expected, actual);
  }

  [Theory]
  [InlineData("en", "US")]
  [InlineData("fr", "FR")]
  [InlineData("de", "DE")]
  public void Constructor_WithCodesAndFromBcp47_ShouldProduceSameResult(string langCode, string regionCode) {
    // Arrange
    Dialect dialect1 = Dialect.FromParts(langCode, regionCode);
    Dialect dialect2 = Dialect.Parse($"{langCode}-{regionCode}");

    // Act
    string result1 = dialect1.ToString();
    string result2 = dialect2.ToString();

    // Assert
    Assert.Equal(result1, result2);
  }

  [Fact]
  public void Constructor_WithNullRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", null);

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithEmptyRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "");

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithWhitespaceRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "   ");

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithNullScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US", null);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Constructor_WithEmptyScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US", "");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Constructor_WithWhitespaceScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = Dialect.FromParts("en", "US", "   ");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Properties_ShouldBeSettable() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en");
    Language french = Languages.Instance.GetLanguage("fr");
    Area canada = Regions.Instance.GetArea("CA");
    Script latn = Scripts.Instance.GetFromCode("Latn");

    // Act
    dialect.Language = french;
    dialect.Region = canada;
    dialect.Script = latn;
    dialect.Variant = "test";

    // Assert
    Assert.Equal(french, dialect.Language);
    Assert.Equal(canada, dialect.Region);
    Assert.Equal(latn, dialect.Script);
    Assert.Equal("test", dialect.Variant);
  }

  [Fact]
  public void ToString_WithM49RegionCode_ShouldUseM49Code() {
    string expected = "en";

    // Arrange
    Dialect dialect = Dialect.Parse("en-840");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void Constructor_FromBcp47_WithVariantOnly_ShouldParseCorrectly() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-1234abc");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("Latn", dialect.Script.Code);
    Assert.Null(dialect.Region);
    Assert.Equal("1234abc", dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithEightCharacterVariant_ShouldParseCorrectly() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-abcdefgh");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("abcdefgh", dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithFiveCharacterVariant_ShouldParseCorrectly() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en-abcde");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("abcde", dialect.Variant);
  }

  [Fact]
  public void Constructor_WithLanguageScriptAndRegionObjects_ShouldCreateDialect() {
    // Arrange
    Language spanish = Languages.Instance.GetLanguage("es");
    Script latn = Scripts.Instance.GetFromCode("Latn");
    Area mexico = Regions.Instance.GetArea("MX");

    // Act
    Dialect dialect = new(spanish, mexico, latn);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal(spanish, dialect.Language);
    Assert.Equal(latn, dialect.Script);
    Assert.Equal(mexico, dialect.Region);
  }

  [Fact]
  public void Constructor_FromBcp47_WithComplexScenarios_ShouldHandleCorrectly() {
    // Arrange & Act
    Dialect dialect1 = Dialect.Parse("sr-Cyrl-RS");
    Dialect dialect2 = Dialect.Parse("sr-Latn-RS");

    // Assert
    Assert.Equal("sr", dialect1.Language.Part1Code);
    Assert.Equal("Cyrl", dialect1.Script.Code);
    Assert.Equal("RS", dialect1.Region.Alpha2Code);

    Assert.Equal("sr", dialect2.Language.Part1Code);
    Assert.Equal("Latn", dialect2.Script.Code);
    Assert.Equal("RS", dialect2.Region.Alpha2Code);
  }

  [Theory]
  [InlineData("en-GB", "en", "GB")]
  [InlineData("zh-CN", "zh", "CN")]
  [InlineData("pt-BR", "pt", "BR")]
  [InlineData("es-MX", "es", "MX")]
  public void Constructor_FromBcp47_WithCommonLanguageRegionPairs_ShouldParse(string bcp47, string expectedLang, string expectedRegion) {
    // Arrange & Act
    Dialect dialect = Dialect.Parse(bcp47);

    // Assert
    Assert.Equal(expectedLang, dialect.Language.Part1Code);
    Assert.Equal(expectedRegion, dialect.Region.Alpha2Code);
  }

  [Fact]
  public void ToString_AfterPropertyModification_ShouldReflectChanges() {
    string expected = "en";

    // Arrange
    Dialect dialect = Dialect.Parse("en");
    Area usa = Regions.Instance.GetArea("US");

    // Act
    dialect.Region = usa;
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void Constructor_WithMinimalValidBcp47_ShouldSucceed() {
    // Arrange & Act
    Dialect dialect = Dialect.Parse("en");

    // Assert
    Assert.NotNull(dialect);
    Assert.NotNull(dialect.Language);
  }

  [Fact]
  public void ToString_WithNullLanguage_ShouldUseUndetermined() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");
    Dialect dialect = new(english) {
      Language = null
    };

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.StartsWith("und", result);
  }

  [Fact]
  public void ToString_WithOptions_None_ShouldReturnLanguageOnly() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Act
    string result = dialect.ToString(DialectOptions.None);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_Script_ShouldIncludeScript() {
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", "CN", "Hans");

    // Act
    string result = dialect.ToString(DialectOptions.Script);

    // Assert
    Assert.Equal("zh-Hans", result);
  }

  [Fact]
  public void ToString_WithOptions_Region_ShouldIncludeRegion() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString(DialectOptions.Region);

    // Assert
    Assert.Equal("en-US", result);
  }

  [Fact]
  public void ToString_WithOptions_Variant_ShouldIncludeVariant() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", null, null, "posix");

    // Act
    string result = dialect.ToString(DialectOptions.Variant);

    // Assert
    Assert.Equal("en-posix", result);
  }

  [Fact]
  public void ToString_WithOptions_ScriptAndRegion_ShouldIncludeBoth() {
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", "CN", "Hans");

    // Act
    string result = dialect.ToString(DialectOptions.Script | DialectOptions.Region);

    // Assert
    Assert.Equal("zh-Hans-CN", result);
  }

  [Fact]
  public void ToString_WithOptions_AllComponents_ShouldIncludeAll() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Act
    DialectOptions options = DialectOptions.Script | DialectOptions.Region | DialectOptions.Variant;
    string result = dialect.ToString(options);

    // Assert
    Assert.Equal("en-Latn-US-posix", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguagePreferPart3_ShouldUsePart3Code() {
    // Arrange
    Language language = Languages.Instance.GetLanguage("en");
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3);

    // Assert
    Assert.Equal("eng", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguagePreferPart3WithoutPart1_ShouldUsePart3Code() {
    // Arrange
    Language language = Languages.Instance.GetFromPart3Code("cmn");
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3);

    // Assert
    Assert.Equal("cmn", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49_ShouldUseM49Code() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49);

    // Assert
    Assert.Equal("en-840", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferAlpha3_ShouldUseAlpha3Code() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-USA", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49AndAlpha3_ShouldPrioritizeM49() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49 | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-840", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguagePreferPart3AndAllComponents_ShouldUseAllWithPart3() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3 | DialectOptions.Script | DialectOptions.Region | DialectOptions.Variant);

    // Assert
    Assert.Equal("eng-Latn-US-posix", result);
  }

  [Fact]
  public void ToString_WithOptions_ScriptOnly_WithoutScript_ShouldReturnLanguageOnly() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");
    dialect.Script = null;

    // Act
    string result = dialect.ToString(DialectOptions.Script);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionOnly_WithoutRegion_ShouldReturnLanguageOnly() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en");

    // Act
    string result = dialect.ToString(DialectOptions.Region);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_VariantOnly_WithoutVariant_ShouldReturnLanguageOnly() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US");

    // Act
    string result = dialect.ToString(DialectOptions.Variant);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionAndVariant_ShouldIncludeBoth() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", null, "posix");

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.Variant);

    // Assert
    Assert.Equal("en-US-posix", result);
  }

  [Fact]
  public void ToString_WithOptions_ScriptAndVariant_ShouldIncludeBoth() {
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", null, "Hans", "variant");

    // Act
    string result = dialect.ToString(DialectOptions.Script | DialectOptions.Variant);

    // Assert
    Assert.Equal("zh-Hans-variant", result);
  }

  [Fact]
  public void ToString_WithOptions_ComplexCombination_ShouldFormatCorrectly() {
    // Arrange
    Dialect dialect = Dialect.FromParts("zh", "CN", "Hans", "test");

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3 | DialectOptions.Script | DialectOptions.Region | DialectOptions.RegionPreferAlpha3 | DialectOptions.Variant);

    // Assert
    Assert.Equal("zho-Hans-CHN-test", result);
  }

  [Fact]
  public void ToString_WithOptions_M49RegionCode_WithRegionPreferUnM49_ShouldUseM49() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "840");

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49);

    // Assert
    Assert.Equal("en-840", result);
  }

  [Theory]
  [InlineData(DialectOptions.None, "en")]
  [InlineData(DialectOptions.Script, "en-Latn")]
  [InlineData(DialectOptions.Region, "en-US")]
  [InlineData(DialectOptions.Variant, "en-posix")]
  [InlineData(DialectOptions.Script | DialectOptions.Region, "en-Latn-US")]
  [InlineData(DialectOptions.Script | DialectOptions.Variant, "en-Latn-posix")]
  [InlineData(DialectOptions.Region | DialectOptions.Variant, "en-US-posix")]
  [InlineData(DialectOptions.Script | DialectOptions.Region | DialectOptions.Variant, "en-Latn-US-posix")]
  public void ToString_WithOptions_VariousCombinations_ShouldFormatCorrectly(DialectOptions options, string expected) {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "posix");

    // Act
    string result = dialect.ToString(options);

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToString_WithOptions_NullLanguageAndVariousFlags_ShouldUseUndetermined() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");
    Dialect dialect = new(english, Regions.Instance.GetArea("US")) {
      Language = null
    };

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.Script);

    // Assert
    Assert.StartsWith("und", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguageWithoutPart1Code_ShouldUsePart3() {
    // Arrange
    Language language = Languages.Instance.GetFromPart3Code("cmn");
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString(DialectOptions.None);

    // Assert
    Assert.Equal("cmn", result);
  }

  [Fact]
  public void ToString_WithOptions_EmptyVariant_ShouldNotIncludeVariant() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "");

    // Act
    string result = dialect.ToString(DialectOptions.Variant);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_WhitespaceVariant_ShouldNotIncludeVariant() {
    // Arrange
    Dialect dialect = Dialect.FromParts("en", "US", "Latn", "   ");

    // Act
    string result = dialect.ToString(DialectOptions.Variant);

    // Assert
    Assert.Equal("en", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguagePreferPart3_WithMissingLanguageCodes_ShouldUseUndetermined() {
    // Arrange
    Language language = new("Test", "", "", "", "", "I", "L", "", "Latn", [], []);
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3);

    // Assert
    Assert.Equal("und", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49AndRegionPreferAlpha3_WithNoM49_ShouldUseAlpha3() {
    // Arrange
    Area area = CreateArea(null, null, "USA");
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49 | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-USA", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49_WithNoM49AndNoAlpha2_ShouldFallbackToAlpha3() {
    // Arrange
    Area area = CreateArea(null, null, "USA");
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49);

    // Assert
    Assert.Equal("en-USA", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferAlpha3_WithOnlyM49_ShouldFallbackToM49() {
    // Arrange
    Area area = CreateArea("840", null, null);
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-840", result);
  }

  [Fact]
  public void ToString_WithOptions_Region_WithOnlyM49_ShouldFallbackToM49() {
    // Arrange
    Area area = CreateArea("840", null, null);
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region);

    // Assert
    Assert.Equal("en-840", result);
  }

  [Fact]
  public void ToString_WithOptions_LanguagePreferPart3_WithOnlyPart1Code_ShouldUsePart1Code() {
    // Arrange
    Language language = new("Test", "tl", "", "", "", "I", "L", "", "Latn", [], []);
    Dialect dialect = new(language);

    // Act
    string result = dialect.ToString(DialectOptions.LanguagePreferPart3);

    // Assert
    Assert.Equal("tl", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49AndRegionPreferAlpha3_WithOnlyAlpha2_ShouldUseAlpha2() {
    // Arrange
    Area area = CreateArea(null, "US", null);
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49 | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-US", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferUnM49_WithOnlyAlpha2_ShouldUseAlpha2() {
    // Arrange
    Area area = CreateArea(null, "US", null);
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferUnM49);

    // Assert
    Assert.Equal("en-US", result);
  }

  [Fact]
  public void ToString_WithOptions_RegionPreferAlpha3_WithOnlyAlpha2_ShouldFallbackToAlpha2() {
    // Arrange
    Area area = CreateArea(null, "US", null);
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region | DialectOptions.RegionPreferAlpha3);

    // Assert
    Assert.Equal("en-US", result);
  }

  [Fact]
  public void ToString_WithOptions_Region_WithOnlyAlpha3_ShouldFallbackToAlpha3() {
    // Arrange
    Area area = CreateArea(null, null, "USA");
    Dialect dialect = Dialect.FromParts("en");
    dialect.Region = area;

    // Act
    string result = dialect.ToString(DialectOptions.Region);

    // Assert
    Assert.Equal("en-USA", result);
  }

  private static Area CreateArea(string? m49Code, string? alpha2Code, string? alpha3Code) {
    // Arrange
    Area area = new();
    BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

    // Act
    area.GetType().GetProperty(nameof(Area.M49Code), bindingFlags)?.SetValue(area, m49Code);
    area.GetType().GetProperty(nameof(Area.Alpha2Code), bindingFlags)?.SetValue(area, alpha2Code);
    area.GetType().GetProperty(nameof(Area.Alpha3Code), bindingFlags)?.SetValue(area, alpha3Code);

    // Assert
    return area;
  }
}

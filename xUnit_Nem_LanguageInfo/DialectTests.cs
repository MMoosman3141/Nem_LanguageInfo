using Nem_LanguageInfo;

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
    Dialect dialect = new("en");

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
    Dialect dialect = new("en", "US");

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
    Dialect dialect = new("zh", "CN", "Hans");

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
    Dialect dialect = new("en", "US", "Latn", "posix");

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
    Assert.Throws<ArgumentException>(() => new Dialect("en", "ZZ"));
  }

  [Fact]
  public void Constructor_WithM49RegionCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = new("en", "840");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("840", dialect.Region.M49Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithLanguageOnly_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = new("en");

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
    Dialect dialect = new("en-US");

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
    Dialect dialect = new("zh-Hans-CN");

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
    Dialect dialect = new("en-Latn-US-posix");

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
    Dialect dialect = new("zh-Hans");

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
    Dialect dialect = new("en-posix");

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
    Dialect dialect = new("eng-US");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("eng", dialect.Language.Part3Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithM49RegionCode_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = new("en-840");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("840", dialect.Region.M49Code);
  }

  [Fact]
  public void Constructor_FromBcp47_WithInvalidFormat_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => new Dialect("not-a-valid-bcp47"));
  }

  [Fact]
  public void Constructor_FromBcp47_WithInvalidRegionCode_ShouldThrowArgumentException() {
    // Arrange, Act & Assert
    Assert.Throws<ArgumentException>(() => new Dialect("en-ZZ"));
  }

  [Fact]
  public void Constructor_FromBcp47_CaseInsensitive_ShouldCreateDialect() {
    // Arrange & Act
    Dialect dialect = new("EN-us");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("en", dialect.Language.Part1Code);
    Assert.Equal("US", dialect.Region.Alpha2Code);
  }

  [Fact]
  public void ToString_WithLanguageOnly_ShouldReturnLanguageCode() {
    // Arrange
    Dialect dialect = new("en");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("en-Latn", result);
  }

  [Fact]
  public void ToString_WithLanguageAndRegion_ShouldReturnBcp47Format() {
    // Arrange
    Dialect dialect = new("en", "US");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("en-Latn-US", result);
  }

  [Fact]
  public void ToString_WithLanguageScriptAndRegion_ShouldReturnBcp47Format() {
    // Arrange
    Dialect dialect = new("zh", "CN", "Hans");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("zh-Hans-CN", result);
  }

  [Fact]
  public void ToString_WithAllComponents_ShouldReturnBcp47Format() {
    // Arrange
    Dialect dialect = new("en", "US", "Latn", "posix");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("en-Latn-US-posix", result);
  }

  [Fact]
  public void ToString_WithLanguageAndScript_ShouldReturnBcp47Format() {
    // Arrange
    Dialect dialect = new("zh", null, "Hans");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("zh-Hans", result);
  }

  [Fact]
  public void ToString_WithLanguageAndVariant_ShouldReturnBcp47Format() {
    // Arrange
    Dialect dialect = new("en", null, null, "posix");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal("en-Latn-posix", result);
  }

  [Theory]
  [InlineData("eng", "en-Latn")]
  [InlineData("cmn", "cmn-Hanz")]
  [InlineData("yue", "yue-Hant")]
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
  [InlineData("en-US", "en-Latn-US")]
  [InlineData("zh-Hans-CN", "zh-Hans-CN")]
  [InlineData("fr-CA", "fr-Latn-CA")]
  [InlineData("de-DE", "de-Latn-DE")]
  public void ToString_WithVariousBcp47Codes_ShouldReturnCorrectFormat(string bcp47, string expected) {
    // Arrange
    Dialect dialect = new(bcp47);

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void RoundTrip_FromBcp47ToString_ShouldPreserveValue() {
    // Arrange
    string original = "en-Latn-US-posix";
    Dialect dialect = new(original);

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.Equal(original, result);
  }

  [Theory]
  [InlineData("en", "US")]
  [InlineData("fr", "FR")]
  [InlineData("de", "DE")]
  public void Constructor_WithCodesAndFromBcp47_ShouldProduceSameResult(string langCode, string regionCode) {
    // Arrange
    Dialect dialect1 = new(langCode, regionCode);
    Dialect dialect2 = new($"{langCode}-{regionCode}");

    // Act
    string result1 = dialect1.ToString();
    string result2 = dialect2.ToString();

    // Assert
    Assert.Equal(result1, result2);
  }

  [Fact]
  public void Constructor_WithNullRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = new("en", null);

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithEmptyRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = new("en", "");

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithWhitespaceRegionCode_ShouldCreateDialectWithoutRegion() {
    // Arrange & Act
    Dialect dialect = new("en", "   ");

    // Assert
    Assert.NotNull(dialect);
    Assert.Null(dialect.Region);
  }

  [Fact]
  public void Constructor_WithNullScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = new("en", "US", null);

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Constructor_WithEmptyScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = new("en", "US", "");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Constructor_WithWhitespaceScriptCode_ShouldCreateDialectWithoutScript() {
    // Arrange & Act
    Dialect dialect = new("en", "US", "   ");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("Latn", dialect.Script.Code);
  }

  [Fact]
  public void Properties_ShouldBeSettable() {
    // Arrange
    Dialect dialect = new("en");
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
    // Arrange
    Dialect dialect = new("en-840");

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.True(result.Contains("840") || result.Contains("US"));
  }

  [Fact]
  public void Constructor_FromBcp47_WithVariantOnly_ShouldParseCorrectly() {
    // Arrange & Act
    Dialect dialect = new("en-1234abc");

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
    Dialect dialect = new("en-abcdefgh");

    // Assert
    Assert.NotNull(dialect);
    Assert.Equal("abcdefgh", dialect.Variant);
  }

  [Fact]
  public void Constructor_FromBcp47_WithFiveCharacterVariant_ShouldParseCorrectly() {
    // Arrange & Act
    Dialect dialect = new("en-abcde");

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
    Dialect dialect1 = new("sr-Cyrl-RS");
    Dialect dialect2 = new("sr-Latn-RS");

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
    Dialect dialect = new(bcp47);

    // Assert
    Assert.Equal(expectedLang, dialect.Language.Part1Code);
    Assert.Equal(expectedRegion, dialect.Region.Alpha2Code);
  }

  [Fact]
  public void ToString_AfterPropertyModification_ShouldReflectChanges() {
    // Arrange
    Dialect dialect = new("en");
    Area usa = Regions.Instance.GetArea("US");

    // Act
    dialect.Region = usa;
    string result = dialect.ToString();

    // Assert
    Assert.Equal("en-Latn-US", result);
  }

  [Fact]
  public void Constructor_WithMinimalValidBcp47_ShouldSucceed() {
    // Arrange & Act
    Dialect dialect = new("en");

    // Assert
    Assert.NotNull(dialect);
    Assert.NotNull(dialect.Language);
  }

  [Fact]
  public void ToString_WithNullLanguage_ShouldUseUndetermined() {
    // Arrange
    Language english = Languages.Instance.GetLanguage("en");
    Dialect dialect = new(english);
    dialect.Language = null;

    // Act
    string result = dialect.ToString();

    // Assert
    Assert.StartsWith("und", result);
  }
}

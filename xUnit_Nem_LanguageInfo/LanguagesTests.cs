using Nem_LanguageInfo;

namespace xUnit_Nem_LanguageInfo; 

public class LanguagesTests {
  [Fact]
  public void Instance_ShouldReturnSingletonInstance() {
    // Arrange & Act
    Languages instance1 = Languages.Instance;
    Languages instance2 = Languages.Instance;

    // Assert
    Assert.NotNull(instance1);
    Assert.Same(instance1, instance2);
  }

  [Fact]
  public void GetFromName_WithValidName_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language english = languages.GetFromName("English");

    // Assert
    Assert.NotNull(english);
    Assert.Equal("English", english.Name);
    Assert.Equal("en", english.Part1Code);
    Assert.Equal("eng", english.Part3Code);
  }

  [Fact]
  public void GetFromName_WithAlias_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language german = languages.GetFromName("German");

    // Assert
    Assert.NotNull(german);
    Assert.Equal("de", german.Part1Code);
    Assert.Equal("deu", german.Part3Code);
  }

  [Fact]
  public void GetFromName_WithInvalidName_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetFromName("InvalidLanguageName");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetFromPart1Code_WithValidCode_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language english = languages.GetFromPart1Code("en");

    // Assert
    Assert.NotNull(english);
    Assert.Equal("English", english.Name);
    Assert.Equal("en", english.Part1Code);
  }

  [Fact]
  public void GetFromPart1Code_WithInvalidCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetFromPart1Code("zz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetFromPart2BCode_WithValidCode_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language german = languages.GetFromPart2BCode("ger");

    // Assert
    Assert.NotNull(german);
    Assert.Equal("de", german.Part1Code);
    Assert.Equal("ger", german.Part2BCode);
  }

  [Fact]
  public void GetFromPart2BCode_WithInvalidCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetFromPart2BCode("zzz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetFromPart2TCode_WithValidCode_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language german = languages.GetFromPart2TCode("deu");

    // Assert
    Assert.NotNull(german);
    Assert.Equal("de", german.Part1Code);
    Assert.Equal("deu", german.Part2TCode);
  }

  [Fact]
  public void GetFromPart2TCode_WithInvalidCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetFromPart2TCode("zzz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetFromPart3Code_WithValidCode_ShouldReturnCorrectLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language english = languages.GetFromPart3Code("eng");

    // Assert
    Assert.NotNull(english);
    Assert.Equal("English", english.Name);
    Assert.Equal("eng", english.Part3Code);
  }

  [Fact]
  public void GetFromPart3Code_WithInvalidCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetFromPart3Code("zzz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Theory]
  [InlineData("en", "English", "en")]
  [InlineData("fr", "French", "fr")]
  [InlineData("es", "Spanish", "es")]
  public void GetLanguage_WithTwoCharacterCode_ShouldUsePart1Code(string code, string expectedName, string expectedPart1Code) {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage(code);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedName, result.Name);
    Assert.Equal(expectedPart1Code, result.Part1Code);
  }

  [Theory]
  [InlineData("eng", "English", "eng")]
  [InlineData("fra", "French", "fra")]
  [InlineData("spa", "Spanish", "spa")]
  public void GetLanguage_WithThreeCharacterCode_ShouldUsePart3Code(string code, string expectedName, string expectedPart3Code) {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage(code);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedName, result.Name);
    Assert.Equal(expectedPart3Code, result.Part3Code);
  }

  [Fact]
  public void GetLanguage_WithThreeCharacterCode_ShouldFallbackToPart2B() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage("ger");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("de", result.Part1Code);
    Assert.Equal("ger", result.Part2BCode);
  }

  [Theory]
  [InlineData("English")]
  [InlineData("French")]
  [InlineData("Spanish")]
  public void GetLanguage_WithFullName_ShouldReturnCorrectLanguage(string languageName) {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage(languageName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(languageName, result.Name);
  }

  [Fact]
  public void GetLanguage_WithInvalidTwoCharacterCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage("zz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetLanguage_WithInvalidThreeCharacterCode_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage("zzz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetLanguage_WithInvalidName_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage("InvalidLanguage");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Fact]
  public void GetLanguage_WithEmptyString_ShouldReturnUndeterminedLanguage() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage("");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("und", result.Part3Code);
  }

  [Theory]
  [InlineData("en")]
  [InlineData("eng")]
  [InlineData("English")]
  public void GetLanguage_WithDifferentFormats_ShouldReturnSameLanguage(string input) {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language result = languages.GetLanguage(input);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("English", result.Name);
    Assert.Equal("en", result.Part1Code);
    Assert.Equal("eng", result.Part3Code);
  }

  [Fact]
  public void Language_Properties_ShouldBePopulated() {
    // Arrange
    Languages languages = Languages.Instance;

    // Act
    Language english = languages.GetFromPart1Code("en");

    // Assert
    Assert.NotNull(english.Name);
    Assert.NotNull(english.Part1Code);
    Assert.NotNull(english.Part3Code);
    Assert.NotNull(english.Scope);
    Assert.NotNull(english.Type);
    Assert.NotNull(english.Aliases);
  }
}

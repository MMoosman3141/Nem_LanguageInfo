using Nem_LanguageInfo;

namespace xUnit_Nem_LanguageInfo;

public class ScriptsTests {
  [Fact]
  public void Instance_ShouldReturnSingletonInstance() {
    // Arrange & Act
    Scripts instance1 = Scripts.Instance;
    Scripts instance2 = Scripts.Instance;

    // Assert
    Assert.NotNull(instance1);
    Assert.Same(instance1, instance2);
  }

  [Fact]
  public void GetFromCode_WithValidCode_ShouldReturnCorrectScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script latin = scripts.GetFromCode("Latn");

    // Assert
    Assert.NotNull(latin);
    Assert.Equal("Latn", latin.Code);
    Assert.Equal("Latin", latin.Name);
    Assert.Equal(215, latin.Number);
  }

  [Fact]
  public void GetFromCode_WithInvalidCode_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromCode("Zzzz");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
    Assert.Equal(999, result.Number);
  }

  [Fact]
  public void GetFromCode_WithNonExistentCode_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromCode("Xxxx");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
    Assert.Equal(999, result.Number);
  }

  [Fact]
  public void GetFromName_WithValidName_ShouldReturnCorrectScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script latin = scripts.GetFromName("Latin");

    // Assert
    Assert.NotNull(latin);
    Assert.Equal("Latin", latin.Name);
    Assert.Equal("Latn", latin.Code);
    Assert.Equal(215, latin.Number);
  }

  [Fact]
  public void GetFromName_WithAlias_ShouldReturnCorrectScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script arabic = scripts.GetFromName("Arabic");

    // Assert
    Assert.NotNull(arabic);
    Assert.Equal("Arab", arabic.Code);
    Assert.Equal(160, arabic.Number);
  }

  [Fact]
  public void GetFromName_WithInvalidName_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromName("InvalidScriptName");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
    Assert.Equal(999, result.Number);
  }

  [Fact]
  public void GetFromNumber_WithValidNumber_ShouldReturnCorrectScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script latin = scripts.GetFromNumber(215);

    // Assert
    Assert.NotNull(latin);
    Assert.Equal("Latn", latin.Code);
    Assert.Equal("Latin", latin.Name);
    Assert.Equal(215, latin.Number);
  }

  [Fact]
  public void GetFromNumber_WithInvalidNumber_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromNumber(9999);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
    Assert.Equal(999, result.Number);
  }

  [Theory]
  [InlineData("Latn", "Latin", 215)]
  [InlineData("Arab", "Arabic", 160)]
  [InlineData("Cyrl", "Cyrillic", 220)]
  public void GetFromCode_WithMultipleValidCodes_ShouldReturnCorrectScripts(string code, string expectedName, int expectedNumber) {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromCode(code);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(code, result.Code);
    Assert.Equal(expectedName, result.Name);
    Assert.Equal(expectedNumber, result.Number);
  }

  [Theory]
  [InlineData("Latin", "Latn")]
  [InlineData("Arabic", "Arab")]
  [InlineData("Cyrillic", "Cyrl")]
  public void GetFromName_WithMultipleValidNames_ShouldReturnCorrectScripts(string name, string expectedCode) {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromName(name);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(name, result.Name);
    Assert.Equal(expectedCode, result.Code);
  }

  [Theory]
  [InlineData(215, "Latn")]
  [InlineData(160, "Arab")]
  [InlineData(220, "Cyrl")]
  public void GetFromNumber_WithMultipleValidNumbers_ShouldReturnCorrectScripts(int number, string expectedCode) {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromNumber(number);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(number, result.Number);
    Assert.Equal(expectedCode, result.Code);
  }

  [Fact]
  public void Script_Properties_ShouldBePopulated() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script latin = scripts.GetFromCode("Latn");

    // Assert
    Assert.NotNull(latin.Code);
    Assert.NotNull(latin.Name);
    Assert.NotNull(latin.Alias);
    Assert.NotNull(latin.Directionality);
    Assert.True(latin.Number > 0);
    Assert.True(latin.Age >= 0);
    Assert.NotEqual(default, latin.Date);
  }

  [Fact]
  public void GetFromCode_ShouldBeCaseSensitive() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script upperCase = scripts.GetFromCode("Latn");
    Script lowerCase = scripts.GetFromCode("latn");

    // Assert
    Assert.NotNull(upperCase);
    Assert.Equal("Latn", upperCase.Code);
    
    Assert.NotNull(lowerCase);
    Assert.Equal("Latn", lowerCase.Code);
  }

  [Fact]
  public void GetFromName_WithEmptyString_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromName("");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
  }

  [Fact]
  public void GetFromCode_WithEmptyString_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromCode("");

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
  }

  [Fact]
  public void GetFromNumber_WithZero_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromNumber(0);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
  }

  [Fact]
  public void GetFromNumber_WithNegativeNumber_ShouldReturnUnknownScript() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script result = scripts.GetFromNumber(-1);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Zzzz", result.Code);
  }

  [Fact]
  public void Script_DirectionalityProperty_ShouldContainValidValues() {
    // Arrange
    Scripts scripts = Scripts.Instance;

    // Act
    Script latin = scripts.GetFromCode("Latn");
    Script arabic = scripts.GetFromCode("Arab");

    // Assert
    Assert.Equal("LTR", latin.Directionality); // Left-to-Right
    Assert.Equal("RTL", arabic.Directionality); // Right-to-Left
  }
}

using Nem_LanguageInfo;

namespace xUnit_Nem_LanguageInfo; 

public class RegionsTests {
  [Fact]
  public void Instance_ShouldReturnSingletonInstance() {
    // Arrange & Act
    Regions instance1 = Regions.Instance;
    Regions instance2 = Regions.Instance;

    // Assert
    Assert.NotNull(instance1);
    Assert.Same(instance1, instance2);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha2Code_WithValidCode_ShouldReturnCorrectArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromIso3166Alpha2Code("US");

    // Assert
    Assert.NotNull(usa);
    Assert.Equal("US", usa.Alpha2Code);
    Assert.Equal("USA", usa.Alpha3Code);
    Assert.Equal("United States of America", usa.Name);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha2Code_WithInvalidCode_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha2Code("ZZ");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha2Code_WithEmptyString_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha2Code("");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha3Code_WithValidCode_ShouldReturnCorrectArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromIso3166Alpha3Code("USA");

    // Assert
    Assert.NotNull(usa);
    Assert.Equal("US", usa.Alpha2Code);
    Assert.Equal("USA", usa.Alpha3Code);
    Assert.Equal("United States of America", usa.Name);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha3Code_WithInvalidCode_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha3Code("ZZZ");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha3Code_WithEmptyString_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha3Code("");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromName_WithValidName_ShouldReturnCorrectArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromName("United States of America");

    // Assert
    Assert.NotNull(usa);
    Assert.Equal("US", usa.Alpha2Code);
    Assert.Equal("USA", usa.Alpha3Code);
    Assert.Equal("United States of America", usa.Name);
  }

  [Fact]
  public void GetAreaFromName_WithInvalidName_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromName("Invalid Country Name");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromName_WithEmptyString_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromName("");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromUnM49Code_WithValidCode_ShouldReturnCorrectArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromUnM49Code("840");

    // Assert
    Assert.NotNull(usa);
    Assert.Equal("840", usa.M49Code);
    Assert.Equal("United States of America", usa.Name);
  }

  [Fact]
  public void GetAreaFromUnM49Code_WithInvalidCode_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromUnM49Code("9999");

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void GetAreaFromUnM49Code_WithEmptyString_ShouldReturnNull() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromUnM49Code("");

    // Assert
    Assert.Null(result);
  }

  [Theory]
  [InlineData("US", "USA", "United States of America")]
  [InlineData("GB", "GBR", "United Kingdom of Great Britain and Northern Ireland")]
  [InlineData("CA", "CAN", "Canada")]
  [InlineData("DE", "DEU", "Germany")]
  public void GetAreaFromIso3166Alpha2Code_WithMultipleValidCodes_ShouldReturnCorrectAreas(string alpha2, string alpha3, string name) {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha2Code(alpha2);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(alpha2, result.Alpha2Code);
    Assert.Equal(alpha3, result.Alpha3Code);
    Assert.Equal(name, result.Name);
  }

  [Theory]
  [InlineData("USA", "US", "United States of America")]
  [InlineData("GBR", "GB", "United Kingdom of Great Britain and Northern Ireland")]
  [InlineData("CAN", "CA", "Canada")]
  [InlineData("DEU", "DE", "Germany")]
  public void GetAreaFromIso3166Alpha3Code_WithMultipleValidCodes_ShouldReturnCorrectAreas(string alpha3, string alpha2, string name) {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area result = regions.GetAreaFromIso3166Alpha3Code(alpha3);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(alpha3, result.Alpha3Code);
    Assert.Equal(alpha2, result.Alpha2Code);
    Assert.Equal(name, result.Name);
  }

  [Fact]
  public void Area_Properties_ShouldBePopulated() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromIso3166Alpha2Code("US");

    // Assert
    Assert.NotNull(usa.Name);
    Assert.NotNull(usa.Alpha2Code);
    Assert.NotNull(usa.Alpha3Code);
    Assert.NotNull(usa.M49Code);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha2Code_ShouldBeCaseSensitive() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area upperCase = regions.GetAreaFromIso3166Alpha2Code("US");
    Area lowerCase = regions.GetAreaFromIso3166Alpha2Code("us");

    // Assert
    Assert.NotNull(upperCase);
    Assert.Equal("US", upperCase.Alpha2Code);
    
    Assert.NotNull(lowerCase);
    Assert.Equal("US", lowerCase.Alpha2Code);
  }

  [Fact]
  public void GetAreaFromIso3166Alpha3Code_ShouldBeCaseSensitive() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area upperCase = regions.GetAreaFromIso3166Alpha3Code("USA");
    Area lowerCase = regions.GetAreaFromIso3166Alpha3Code("usa");

    // Assert
    Assert.NotNull(upperCase);
    Assert.Equal("USA", upperCase.Alpha3Code);
    
    Assert.NotNull(lowerCase);
    Assert.Equal("USA", lowerCase.Alpha3Code);
  }

  [Fact]
  public void Alpha2AndAlpha3Codes_ShouldReturnSameArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area byAlpha2 = regions.GetAreaFromIso3166Alpha2Code("US");
    Area byAlpha3 = regions.GetAreaFromIso3166Alpha3Code("USA");

    // Assert
    Assert.NotNull(byAlpha2);
    Assert.NotNull(byAlpha3);
    Assert.Same(byAlpha2, byAlpha3);
  }

  [Fact]
  public void NameAndAlpha2Code_ShouldReturnSameArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area byName = regions.GetAreaFromName("United States of America");
    Area byAlpha2 = regions.GetAreaFromIso3166Alpha2Code("US");

    // Assert
    Assert.NotNull(byName);
    Assert.NotNull(byAlpha2);
    Assert.Same(byName, byAlpha2);
  }

  [Fact]
  public void M49CodeAndAlpha3Code_ShouldReturnSameArea() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area byM49 = regions.GetAreaFromUnM49Code("840");
    Area byAlpha3 = regions.GetAreaFromIso3166Alpha3Code("USA");

    // Assert
    Assert.NotNull(byM49);
    Assert.NotNull(byAlpha3);
    Assert.Same(byM49, byAlpha3);
  }

  [Fact]
  public void Area_ShouldHaveDefaultLanguageCode() {
    // Arrange
    Regions regions = Regions.Instance;

    // Act
    Area usa = regions.GetAreaFromIso3166Alpha2Code("US");

    // Assert
    Assert.NotNull(usa);
    Assert.NotNull(usa.DefaultLanguageCode);
    Assert.NotEmpty(usa.DefaultLanguageCode);
  }
}

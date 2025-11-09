# Nem_LanguageInfo

A .NET 8 library for accessing ISO 639 (languages), ISO 3166 (countries), ISO 15924 (scripts), and UN M.49 (regions) standardized information.

## Features

- **ISO 639-3 Language Data**: Access comprehensive language information including codes, names, scopes, types, and aliases
- **ISO 15924 Script Data**: Retrieve script information by code, name, or number
- **ISO 3166 Country Data**: Look up countries by Alpha-2 or Alpha-3 codes
- **UN M.49 Region Data**: Access hierarchical geographic region information
- **Singleton Pattern**: Efficient, thread-safe singleton instances for all data collections
- **Embedded Resources**: All data is embedded in the library for easy deployment

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package Nem_LanguageInfo
```

Or via Package Manager Console:

```powershell
Install-Package Nem_LanguageInfo
```

## Usage

### Languages

Access language information using the `Languages` singleton:

```csharp
using Nem_LanguageInfo;

// Get language by Part 1 code (ISO 639-1, 2-letter codes)
Language english = Languages.Instance.GetFromPart1Code("en");

// Get language by Part 3 code (ISO 639-3, 3-letter codes)
Language french = Languages.Instance.GetFromPart3Code("fra");

// Get language by name
Language spanish = Languages.Instance.GetFromName("Spanish");

// Smart lookup - automatically detects code length or uses name
Language german = Languages.Instance.GetLanguage("de");      // 2-letter code
Language italian = Languages.Instance.GetLanguage("ita");    // 3-letter code
Language japanese = Languages.Instance.GetLanguage("Japanese"); // name

// Access language properties
Console.WriteLine($"Name: {english.Name}");
Console.WriteLine($"Part1: {english.Part1Code}");
Console.WriteLine($"Part3: {english.Part3Code}");
Console.WriteLine($"Scope: {english.Scope}");
Console.WriteLine($"Type: {english.Type}");
Console.WriteLine($"Default Script: {english.DefaultScript}");
Console.WriteLine($"Script Object: {english.Script.Name}");
```

### Scripts

Access script information using the `Scripts` singleton:

```csharp
using Nem_LanguageInfo;

// Get script by 4-letter code
Script latin = Scripts.Instance.GetFromCode("Latn");

// Get script by name
Script cyrillic = Scripts.Instance.GetFromName("Cyrillic");

// Get script by numeric code
Script arabic = Scripts.Instance.GetFromNumber(160);

// Access script properties
Console.WriteLine($"Code: {latin.Code}");
Console.WriteLine($"Name: {latin.Name}");
Console.WriteLine($"Number: {latin.Number}");
Console.WriteLine($"Alias: {latin.Alias}");
Console.WriteLine($"Directionality: {latin.Directionality}");
```

### Regions and Countries

Access geographic region and country information using the `Regions` singleton:

```csharp
using Nem_LanguageInfo;

// Get country by Alpha-2 code
Area usa = Regions.Instance.GetAreaFromAlpha2("US");

// Get country by Alpha-3 code
Area canada = Regions.Instance.GetAreaFromAlpha3("CAN");

// Get region by UN M.49 code
Area europe = Regions.Instance.GetAreaFromM49("150");

// Get area by name
Area africa = Regions.Instance.GetAreaFromName("Africa");

// Access area properties
Console.WriteLine($"Name: {usa.Name}");
Console.WriteLine($"Alpha-2: {usa.Alpha2Code}");
Console.WriteLine($"Alpha-3: {usa.Alpha3Code}");
Console.WriteLine($"M49 Code: {usa.M49Code}");
Console.WriteLine($"Default Language: {usa.DefaultLanguageCode}");
```

## API Reference

### Language Class

Properties:

- `Name` - Language name
- `Part1Code` - ISO 639-1 two-letter code
- `Part2TCode` - ISO 639-2/T three-letter terminology code
- `Part2BCode` - ISO 639-2/B three-letter bibliographic code
- `Part3Code` - ISO 639-3 three-letter code
- `Scope` - Language scope (individual, macrolanguage, special, etc.)
- `Type` - Language type (living, extinct, ancient, etc.)
- `Comment` - Additional comments
- `DefaultScript` - Default script code
- `Script` - Script object for the default script
- `Aliases` - List of alternative names

### Languages Class (Singleton)

Methods:

- `GetFromName(string)` - Get language by name or alias
- `GetFromPart1Code(string)` - Get language by ISO 639-1 code
- `GetFromPart2BCode(string)` - Get language by ISO 639-2/B code
- `GetFromPart2TCode(string)` - Get language by ISO 639-2/T code
- `GetFromPart3Code(string)` - Get language by ISO 639-3 code
- `GetLanguage(string)` - Smart lookup by code or name

### Script Class

Properties:

- `Code` - ISO 15924 four-letter code
- `Number` - ISO 15924 numeric code
- `Name` - Script name
- `Alias` - Alternative name
- `Age` - Script age
- `Date` - Registration date
- `Directionality` - Text direction (LTR, RTL, etc.)

### Scripts Class (Singleton)

Methods:

- `GetFromCode(string)` - Get script by four-letter code
- `GetFromName(string)` - Get script by name or alias
- `GetFromNumber(int)` - Get script by numeric code

### Area Class

Properties:

- `M49Code` - UN M.49 numeric code
- `Name` - Area name
- `Alpha2Code` - ISO 3166-1 Alpha-2 code (for countries)
- `Alpha3Code` - ISO 3166-1 Alpha-3 code (for countries)
- `DefaultLanguageCode` - Primary language code

### Regions Class (Singleton)

Methods:

- `GetAreaFromAlpha2(string)` - Get country by Alpha-2 code
- `GetAreaFromAlpha3(string)` - Get country by Alpha-3 code
- `GetAreaFromM49(string)` - Get area by UN M.49 code
- `GetAreaFromName(string)` - Get area by name

## Data Standards

This library implements the following international standards:

- **ISO 639-3**: Codes for the representation of names of languages (Part 3: Alpha-3 code for comprehensive coverage of languages)
- **ISO 15924**: Codes for the representation of names of scripts
- **ISO 3166-1**: Codes for the representation of names of countries and their subdivisions (Part 1: Country codes)
- **UN M.49**: Standard country or area codes for statistical use

## Requirements

- .NET 8.0 or later

## License

Copyright (c) 2025, Mark Moosman

See [LICENSE.txt](LICENSE.txt) for details.

## Repository

GitHub: [https://github.com/MMoosman3141/Nem_LanguageInfo](https://github.com/MMoosman3141/Nem_LanguageInfo)

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## Author

Mark Moosman
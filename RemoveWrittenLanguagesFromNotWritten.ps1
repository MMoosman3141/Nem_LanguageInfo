param(
    [string]$NotWrittenPath = (Join-Path -Path $PSScriptRoot -ChildPath "Nem_LanguageInfo\Data\iso-639-3_NotWritten.json"),
    [string]$WrittenPath = (Join-Path -Path $PSScriptRoot -ChildPath "Nem_LanguageInfo\Data\iso-639-3_Written.json")
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $NotWrittenPath)) {
    throw "NotWritten file was not found: $NotWrittenPath"
}

if (-not (Test-Path -LiteralPath $WrittenPath)) {
    throw "Written file was not found: $WrittenPath"
}

$notWrittenContent = Get-Content -Raw -LiteralPath $NotWrittenPath
$writtenContent = Get-Content -Raw -LiteralPath $WrittenPath

$notWrittenLanguages = @($notWrittenContent | ConvertFrom-Json)
$writtenLanguages = @($writtenContent | ConvertFrom-Json)

$writtenPart3Codes = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
$writtenNames = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)

foreach ($language in $writtenLanguages) {
    if ($null -ne $language.part3Code -and -not [string]::IsNullOrWhiteSpace([string]$language.part3Code)) {
        [void]$writtenPart3Codes.Add([string]$language.part3Code)
    }

    if ($null -ne $language.name -and -not [string]::IsNullOrWhiteSpace([string]$language.name)) {
        [void]$writtenNames.Add([string]$language.name)
    }
}

$filteredNotWrittenLanguages = foreach ($language in $notWrittenLanguages) {
    $part3Code = if ($null -ne $language.part3Code) { [string]$language.part3Code } else { '' }
    $name = if ($null -ne $language.name) { [string]$language.name } else { '' }

    $existsInWritten = ($part3Code.Length -gt 0 -and $writtenPart3Codes.Contains($part3Code)) -or
                       ($name.Length -gt 0 -and $writtenNames.Contains($name))

    if (-not $existsInWritten) {
        $language
    }
}

$removedCount = $notWrittenLanguages.Count - @($filteredNotWrittenLanguages).Count

$updatedJson = @($filteredNotWrittenLanguages) | ConvertTo-Json -Depth 100
Set-Content -LiteralPath $NotWrittenPath -Value $updatedJson -Encoding UTF8

Write-Host "Removed $removedCount duplicate language entr$(if ($removedCount -eq 1) { 'y' } else { 'ies' }) from '$NotWrittenPath'."
Write-Host "Updated file saved."

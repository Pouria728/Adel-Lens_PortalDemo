[CmdletBinding()]
param(
    [string]$SqlInstance = "localhost",
    [string]$DatabaseName = "HS_Adel",
    [string]$LaunchProfile = "https"
)

$ErrorActionPreference = "Stop"

$projectPath = (Resolve-Path (Join-Path $PSScriptRoot "..\\HamrahanSystem\\HamrahanSystem.Presntation\\HamrahanSystem.Presntation.csproj")).Path
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ConnectionStrings__Default = "Data Source=$SqlInstance;Initial Catalog=$DatabaseName;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Connect Timeout=60"

Write-Host "Starting AdelLens Ordering Portal..."
Write-Host "SQL Server instance: $SqlInstance"
Write-Host "Database: $DatabaseName"
Write-Host "Launch profile: $LaunchProfile"
Write-Host "Login URL: https://localhost:7181/Account/Login"

dotnet run --project $projectPath --launch-profile $LaunchProfile

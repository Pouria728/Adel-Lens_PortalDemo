[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

$projectPath = (Resolve-Path (Join-Path $PSScriptRoot "..\\HamrahanSystem\\HamrahanSystem.Presntation\\HamrahanSystem.Presntation.csproj")).Path

Write-Host "Starting AdelLens lightweight review mode..."
Write-Host "Review URL: http://localhost:5299"

dotnet run --project $projectPath --launch-profile public-demo

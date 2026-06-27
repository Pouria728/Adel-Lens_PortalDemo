[CmdletBinding()]
param(
    [string]$SqlInstance = "localhost",
    [string]$DatabaseName = "HS_Adel",
    [string]$BackupPath = "",
    [switch]$Force
)

$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Data

if ([string]::IsNullOrWhiteSpace($BackupPath)) {
    $BackupPath = Join-Path $PSScriptRoot "..\\database\\HS_Adel_demo.BAK"
}

function Escape-SqlLiteral {
    param([string]$Value)
    return $Value.Replace("'", "''")
}

function Get-ExtensionOrDefault {
    param(
        [string]$Path,
        [string]$DefaultExtension
    )

    $extension = [System.IO.Path]::GetExtension($Path)
    if ([string]::IsNullOrWhiteSpace($extension)) {
        return $DefaultExtension
    }

    return $extension
}

function Invoke-DatabaseScalar {
    param(
        [string]$ConnectionString,
        [string]$Query
    )

    $connection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
    try {
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandText = $Query
        $command.CommandTimeout = 0
        return $command.ExecuteScalar()
    }
    finally {
        $connection.Dispose()
    }
}

function Invoke-DatabaseDataTable {
    param(
        [string]$ConnectionString,
        [string]$Query
    )

    $connection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
    try {
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandText = $Query
        $command.CommandTimeout = 0

        $adapter = New-Object System.Data.SqlClient.SqlDataAdapter $command
        $table = New-Object System.Data.DataTable
        [void]$adapter.Fill($table)
        return ,$table
    }
    finally {
        $connection.Dispose()
    }
}

function Invoke-DatabaseNonQuery {
    param(
        [string]$ConnectionString,
        [string]$Query
    )

    $connection = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
    try {
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandText = $Query
        $command.CommandTimeout = 0
        [void]$command.ExecuteNonQuery()
    }
    finally {
        $connection.Dispose()
    }
}

$resolvedBackupPath = (Resolve-Path $BackupPath).Path
$masterConnectionString = "Server=$SqlInstance;Database=master;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=60"
$escapedBackupPath = Escape-SqlLiteral $resolvedBackupPath
$escapedDatabaseName = Escape-SqlLiteral $DatabaseName
$databaseNameForSql = $DatabaseName.Replace("]", "]]")

Write-Host "Using SQL Server instance: $SqlInstance"
Write-Host "Using backup file: $resolvedBackupPath"

$databaseExists = [int](Invoke-DatabaseScalar -ConnectionString $masterConnectionString -Query "SELECT COUNT(1) FROM sys.databases WHERE name = N'$escapedDatabaseName'")
if ($databaseExists -gt 0 -and -not $Force) {
    throw "Database '$DatabaseName' already exists. Run the script again with -Force to overwrite it."
}

$fileList = Invoke-DatabaseDataTable -ConnectionString $masterConnectionString -Query "RESTORE FILELISTONLY FROM DISK = N'$escapedBackupPath'"
if ($fileList.Rows.Count -eq 0) {
    throw "Could not detect any files inside the backup."
}

$defaultDataPath = Invoke-DatabaseScalar -ConnectionString $masterConnectionString -Query @"
SELECT COALESCE(
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath')),
    LEFT(physical_name, LEN(physical_name) - CHARINDEX('\', REVERSE(physical_name)) + 1)
)
FROM sys.master_files
WHERE database_id = 1 AND file_id = 1;
"@

$defaultLogPath = Invoke-DatabaseScalar -ConnectionString $masterConnectionString -Query @"
SELECT COALESCE(
    CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultLogPath')),
    LEFT(physical_name, LEN(physical_name) - CHARINDEX('\', REVERSE(physical_name)) + 1)
)
FROM sys.master_files
WHERE database_id = 1 AND file_id = 2;
"@

if ([string]::IsNullOrWhiteSpace($defaultDataPath)) {
    throw "Could not determine the SQL Server default data path."
}

if ([string]::IsNullOrWhiteSpace($defaultLogPath)) {
    throw "Could not determine the SQL Server default log path."
}

$moveClauses = New-Object System.Collections.Generic.List[string]
$restoredFiles = New-Object System.Collections.Generic.List[string]
$dataIndex = 0
$logIndex = 0
$fileRows = $fileList.Select()

foreach ($row in $fileRows) {
    $logicalName = [string]$row["LogicalName"]
    $physicalName = [string]$row["PhysicalName"]
    $fileType = [string]$row["Type"]

    if ($fileType -eq "L") {
        $logIndex++
        $extension = Get-ExtensionOrDefault -Path $physicalName -DefaultExtension ".ldf"
        $targetName = if ($logIndex -eq 1) { "$DatabaseName`_log$extension" } else { "$DatabaseName`_log_$logIndex$extension" }
        $targetPath = Join-Path $defaultLogPath $targetName
    }
    else {
        $dataIndex++
        $defaultExtension = if ($dataIndex -eq 1) { ".mdf" } else { ".ndf" }
        $extension = Get-ExtensionOrDefault -Path $physicalName -DefaultExtension $defaultExtension
        $targetName = if ($dataIndex -eq 1) { "$DatabaseName$extension" } else { "$DatabaseName`_$dataIndex$extension" }
        $targetPath = Join-Path $defaultDataPath $targetName
    }

    $escapedLogicalName = Escape-SqlLiteral $logicalName
    $escapedTargetPath = Escape-SqlLiteral $targetPath
    $moveClauses.Add("MOVE N'$escapedLogicalName' TO N'$escapedTargetPath'")
    $restoredFiles.Add($targetPath)
}

$moveClauseSql = [string]::Join(",`r`n", $moveClauses)

if ($databaseExists -gt 0) {
    Write-Host "Existing database detected. Switching to SINGLE_USER mode before restore."
    Invoke-DatabaseNonQuery -ConnectionString $masterConnectionString -Query "ALTER DATABASE [$databaseNameForSql] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
}

try {
    Write-Host "Restoring database '$DatabaseName'..."
    Invoke-DatabaseNonQuery -ConnectionString $masterConnectionString -Query @"
RESTORE DATABASE [$databaseNameForSql]
FROM DISK = N'$escapedBackupPath'
WITH REPLACE,
$moveClauseSql,
RECOVERY,
STATS = 5;
"@
}
finally {
    $restoredExists = [int](Invoke-DatabaseScalar -ConnectionString $masterConnectionString -Query "SELECT COUNT(1) FROM sys.databases WHERE name = N'$escapedDatabaseName'")
    if ($restoredExists -gt 0) {
        Invoke-DatabaseNonQuery -ConnectionString $masterConnectionString -Query "ALTER DATABASE [$databaseNameForSql] SET MULTI_USER;"
    }
}

Write-Host "Database restore completed."
Write-Host "Database name: $DatabaseName"
Write-Host "Restored files:"
$restoredFiles | ForEach-Object { Write-Host " - $_" }

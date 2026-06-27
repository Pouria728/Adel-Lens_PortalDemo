# Local Setup Guide

This guide describes the fastest way to run the full AdelLens Ordering Portal locally from the public repository.

## Recommended Environment

- Windows 10 or Windows 11
- .NET 8 SDK
- SQL Server Developer or SQL Server Express
- PowerShell

## Full Application Setup

### Restore the sample database

From the repository root:

```powershell
.\scripts\Restore-AdelLensDemoDatabase.ps1
```

This restores the included backup file into a local SQL Server database named `HS_Adel`.

If the database already exists and you want to replace it:

```powershell
.\scripts\Restore-AdelLensDemoDatabase.ps1 -Force
```

### Start the application

```powershell
.\scripts\Start-AdelLensPortal.ps1
```

The script sets a local SQL Server connection string and starts the application with the `https` launch profile.

Default login page:

- `https://localhost:7181/Account/Login`

### Sample local credentials

- Username: `admin`
- Password: `Admin123!`

## Optional UI-Only Review Mode

If you only want to inspect the UI without restoring SQL Server data:

```powershell
.\scripts\Start-AdelLensReviewMode.ps1
```

Default URL:

- `http://localhost:5299`

## Troubleshooting

### SQL Server connection problems

- Use `localhost` as the SQL Server host for the included scripts
- Make sure the SQL Server service is running before restoring the backup
- If you use a named instance, pass it explicitly:

```powershell
.\scripts\Restore-AdelLensDemoDatabase.ps1 -SqlInstance ".\\SQLEXPRESS"
.\scripts\Start-AdelLensPortal.ps1 -SqlInstance ".\\SQLEXPRESS"
```

### HTTPS development certificate warning

If your machine does not trust the local ASP.NET certificate yet:

```powershell
dotnet dev-certs https --trust
```

### Redis availability

Redis is not required for the local portfolio setup. The application is already configured to use in-memory caching when Redis is disabled.

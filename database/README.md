# Sample Database Package

This folder contains the local demo database backup used by the portfolio version of AdelLens Ordering Portal.

## Included File

- `HS_Adel_demo.BAK`

## Purpose

The backup is provided so reviewers can restore the same demo dataset used during local testing and screenshot capture.

## Restore

Use the helper script from the repository root:

```powershell
.\scripts\Restore-AdelLensDemoDatabase.ps1
```

This restores the backup into a local SQL Server database named `HS_Adel`.

## Notes

- This package is intended for local portfolio review only
- It is not meant to represent production infrastructure
- The application should be started afterward with `.\scripts\Start-AdelLensPortal.ps1`

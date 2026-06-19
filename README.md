# AdelLens Ordering Portal Demo

Portfolio-ready demo of an ASP.NET Core MVC web application for optical lens ordering, workflow tracking, and custom lens request management.

## Highlights

- Custom lens order workflow
- Order listing and request tracking
- Role-aware back-office structure
- Local demo mode for portfolio presentation
- SQL Server backed application mode for full testing

## Tech Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Redis optional for local development

## Repository Notes

This repository is prepared as a sanitized demo version for portfolio use.

- Real infrastructure values and internal connection strings are intentionally removed
- Public demo mode can run without a database
- Full application mode requires a local SQL Server database and a valid connection string
- The original business UI is localized for Persian-speaking users, while the portfolio-facing documentation and demo entry points are in English

## Run The Public Demo

From the `Project/AdelLens web/AdelWeb` directory:

```powershell
dotnet run --project "HamrahanSystem/HamrahanSystem.Presntation/HamrahanSystem.Presntation.csproj" --launch-profile public-demo
```

Demo URL:

- `http://localhost:5299`

## Run The Full Application

Set a local connection string first:

```powershell
$env:ConnectionStrings__Default="Data Source=.;Initial Catalog=HS_Adel;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Connect Timeout=60"
```

You can use [`appsettings.Example.json`](HamrahanSystem/HamrahanSystem.Presntation/appsettings.Example.json) as a reference for local configuration.

Then run:

```powershell
dotnet run --project "HamrahanSystem/HamrahanSystem.Presntation/HamrahanSystem.Presntation.csproj" --launch-profile https
```

If you do not want Redis locally, the app is already configured to use in-memory caching when `Infrastructure:UseRedis` is `false`.

## Recommended Screenshots For GitHub

- Login page
- Custom lens order screen
- Order list screen
- Workflow or process management screen
- Public demo landing page

## What To Keep Private

- Real database backups
- Internal SQL Server addresses
- Real usernames and passwords
- Customer or production data
- Internal deployment scripts and environment-specific secrets

## Portfolio Positioning

Suggested short description for CV or LinkedIn:

> Developed and prepared a demo-ready lens ordering and workflow management web application using ASP.NET Core MVC, SQL Server, and custom business process flows.

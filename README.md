# AdelLens Ordering Portal

A portfolio-ready ASP.NET Core MVC web application for optical lens ordering, workflow tracking, and custom lens request management.

## Overview

AdelLens Ordering Portal is a business-oriented web application built to support operational workflows around optical lens ordering and internal process management.

The system includes order tracking, custom lens request flows, workflow-driven progression, and back-office administration for related operational data. This public repository is a sanitized portfolio version of the project, prepared for technical presentation without exposing private infrastructure or operational data.

## Key Features

- Custom lens ordering workflow
- Order listing and review
- Workflow-driven process tracking
- Ready lens and warranty-related order handling
- Design type and configuration management
- Role-aware back-office structure
- Public demo mode for portfolio presentation
- Full application mode for local technical evaluation

## Tech Stack

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Optional Redis-backed caching
- Razor Views / Server-rendered UI

## Screenshots

### Login Page
Internal operator login screen for accessing the portal.

![Login Page](docs/screenshots/login-page.png)

### Dashboard and Main Navigation
Main dashboard and navigation layout for accessing the core operational modules.

![Dashboard and Main Navigation](docs/screenshots/dashboard-main-menu.png)

### Custom Lens Order
Custom lens ordering workflow with business-specific inputs and request handling.

![Custom Lens Order](docs/screenshots/custom-lens-order.png)

### Order Review
Order tracking and review screen for monitoring submitted requests and their current status.

![Order Review](docs/screenshots/order-review.png)

### Ready Lens with Warranty
Operational page for managing ready lens orders with warranty-related workflow support.

![Ready Lens with Warranty](docs/screenshots/ready-lens-warranty.png)

### Design Type Settings
Administrative configuration screen for managing design type definitions and related setup data.

![Design Type Settings](docs/screenshots/design-type-settings.png)

## Public Demo

A public demo mode is included for quick portfolio review and does not require a database connection.

Run from the `Project/AdelLens web/AdelWeb` directory:

```powershell
dotnet run --project "HamrahanSystem/HamrahanSystem.Presntation/HamrahanSystem.Presntation.csproj" --launch-profile public-demo
```

Demo URL:

- `http://localhost:5299`

## Full Application Setup

To run the full application locally, set a valid SQL Server connection string first:

```powershell
$env:ConnectionStrings__Default="Data Source=.;Initial Catalog=HS_Adel;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Connect Timeout=60"
```

Use [`appsettings.Example.json`](HamrahanSystem/HamrahanSystem.Presntation/appsettings.Example.json) as a reference for local configuration.

Then run:

```powershell
dotnet run --project "HamrahanSystem/HamrahanSystem.Presntation/HamrahanSystem.Presntation.csproj" --launch-profile https
```

If Redis is not available locally, the application can use in-memory caching by keeping `Infrastructure:UseRedis` set to `false`.

## Repository Notes

- This repository is published as a portfolio-safe public mirror
- Real infrastructure values and internal connection strings have been removed
- Production data, credentials, and internal-only environment details are not included
- The original business UI targets Persian-speaking users, while the portfolio-facing documentation is provided in English

## Contribution Highlights

This portfolio version showcases work around:

- Running and validating the application locally
- Preparing a public GitHub-safe version of the project
- Improving demo readiness and local setup clarity
- Fixing runtime issues discovered during testing
- Presenting a real-world business application in a cleaner external format

## CV / LinkedIn Summary

> Developed and prepared a portfolio-ready ASP.NET Core MVC portal for optical lens ordering, workflow tracking, and custom product request management.

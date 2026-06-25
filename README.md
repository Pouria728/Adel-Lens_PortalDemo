# AdelLens Ordering Portal

A portfolio-ready ASP.NET Core MVC application for optical lens ordering, workflow tracking, and custom lens request management.

## Overview

AdelLens Ordering Portal is a business-oriented web application built to support operational workflows around optical lens ordering and internal process management.

The system includes order tracking, custom lens request flows, workflow-based progression, and back-office features for managing related operational data.

This public repository is a sanitized portfolio version of the project. Sensitive infrastructure values, production-specific configuration, and private operational data have been removed.

## Key Features

- Custom lens ordering workflow
- Order listing and request tracking
- Workflow-driven order progression
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

## Public Demo

A public demo mode is included for quick portfolio review and does not require a database connection.

Run from the `Project/AdelLens web/AdelWeb` directory:

```powershell
dotnet run --project "HamrahanSystem/HamrahanSystem.Presntation/HamrahanSystem.Presntation.csproj" --launch-profile public-demo

## Screenshots

### Login Page
![Login Page](docs/screenshots/login-page.png)

### Dashboard and Main Navigation
![Dashboard and Main Navigation](docs/screenshots/dashboard-main-menu.png)

### Custom Lens Order
![Custom Lens Order](docs/screenshots/custom-lens-order.png)

### Order Review
![Order Review](docs/screenshots/order-review.png)

### Ready Lens with Warranty
![Ready Lens with Warranty](docs/screenshots/ready-lens-warranty.png)

### Design Type Settings
![Design Type Settings](docs/screenshots/design-type-settings.png)

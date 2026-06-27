# Architecture Overview

This document provides a concise technical walkthrough of the portfolio version of AdelLens Ordering Portal.

## Solution Layout

The solution follows a layered ASP.NET Core structure:

- `HamrahanSystem.Presntation`
  ASP.NET Core MVC presentation layer containing controllers, Razor views, middleware, and startup configuration.
- `HamrahanSystem.Application`
  Application services, use case interfaces and implementations, DTO conversion, and orchestration logic.
- `HamrahanSystem.Domain`
  Domain entities, repository interfaces, and business-facing contracts.
- `HamrahanSystem.Infrastructure`
  Entity Framework Core context, repository implementations, dependency wiring, and persistence integration.
- `HamrahanSystem.Extention`
  Shared helper extensions used across the solution.

## Main Functional Areas

The current portfolio snapshot highlights these business modules:

- Account sign-in and role-aware access flow
- Order listing and order review
- Custom lens ordering and related configuration entities
- Ready lens and warranty handling
- Process and workflow management
- Master-data administration such as design types, lens types, material types, coatings, and related lookup data

## Runtime Flow

At a high level, the request flow is:

1. The MVC presentation layer accepts the request and handles routing, session state, and authentication.
2. Controllers call application services and use-case interfaces from the application layer.
3. The application layer coordinates business operations against domain contracts.
4. Infrastructure components persist and retrieve data through Entity Framework Core and SQL Server.

## Portfolio-Specific Adjustments

This public repository includes a small number of portfolio-oriented adjustments so reviewers can run it safely:

- Local helper scripts for database restore and application startup
- A sample SQL Server backup for reproducible local setup
- An optional lightweight review mode that does not require a database
- In-memory cache fallback when Redis is disabled
- Public-facing English documentation while preserving the original Persian business UI

## Why The Demo Is Local-First

The main portfolio target is the real application flow rather than a static showcase clone. Because the application depends on ASP.NET Core server behavior, authentication, and SQL-backed workflows, the repository is packaged so reviewers can run the full stack locally with minimal setup.

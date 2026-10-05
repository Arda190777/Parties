# Parties

**Party planning and guest invitations with ASP.NET Core MVC.**

A C# web application demonstrating server-rendered MVC, ASP.NET Identity, relational persistence and guest-response workflows.

## Features

- Party creation, editing, detail views and administrator-restricted deletion.
- Invitation management and guest responses.
- Account authentication and role-restricted actions with ASP.NET Identity.
- Razor views, model validation and EF Core migrations.
- xUnit controller tests in a separate test project.

**Stack:** .NET 8 · C# · ASP.NET Core MVC · Razor · Entity Framework Core · SQLite · ASP.NET Identity · xUnit

## Run locally

Install the .NET 8 SDK and the EF Core 8 CLI tool if it is not already available.

```powershell
git clone https://github.com/Arda190777/Parties.git
cd Parties
dotnet restore Partys.sln
dotnet tool install --global dotnet-ef --version 8.0.0
dotnet ef database update --project Partys/Partys.csproj --startup-project Partys/Partys.csproj
dotnet run --project Partys/Partys.csproj --launch-profile https
```

Use the URL printed by ASP.NET; the HTTPS launch profile currently uses https://localhost:7169. Check the development database connection in `Partys/appsettings.json` before applying migrations.

## Verify

```powershell
dotnet build Partys.sln
dotnet test Partys.sln
```

## Code map

```text
Partys/Controllers/     Party, invitation and home routes
Partys/Models/          Entities and input/view models
Partys/Views/           Razor UI
Partys/Data/            EF Core context and Identity setup
Partys/Migrations/      Database schema history
Partys/Services/        Invitation email abstraction
Partys.Tests/           xUnit controller tests
```

The current email service logs invitation messages and links; it does not send email through a provider. Seeded accounts are development conveniences and require review before any deployment. This repository is a portfolio/learning project, with no standalone license file currently included.

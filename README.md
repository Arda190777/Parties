# 🎉 Parties

A full-stack web application for managing parties and guest invitations,
built with ASP.NET Core MVC and Entity Framework Core.

## ✨ Features

- 🔐 Role-based authentication with ASP.NET Identity (Organiser / Guest)
- 📋 Full CRUD operations for parties and invitations
- 👥 Guest list management with RSVP tracking
- 🛡️ Server-side input validation and exception handling
- 🧪 Unit tests with xUnit
- 🗄️ Code-First database schema with EF Core migrations

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQLite |
| Authentication | ASP.NET Identity |
| Language | C# |
| Frontend | Razor Views · HTML · CSS · JavaScript |
| Testing | xUnit |

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Installation

```bash
git clone https://github.com/Arda190777/Parties.git
cd Parties
dotnet restore
dotnet ef database update
dotnet run
```

Then open your browser at `https://localhost:5001`

## 🧪 Running Tests

```bash
cd Partys.Tests
dotnet test
```

## 📁 Project Structure
Partys/
├── Controllers/
├── Models/
├── Views/
├── Services/
├── Data/
└── wwwroot/
Partys.Tests/
└── (xUnit test files)

# Timesheets

This project is a web application that lets users record the time spent during their working day. It also 
serves as a playground for experimenting with new software engineering tools, technologies and techniques.

## Built With
- [Asp.Net Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [HTMX](https://htmx.org/)
- [Bootstrap](https://getbootstrap.com)
- [Entity Framework Core](https://learn.microsoft.com/en-gb/ef/core)
- [Sonar Cloud](https://www.sonarsource.com/products/sonarcloud)
- [XUnit](https://xunit.net)
- [Reqnroll](https://reqnroll.net/)
- [Playwright for .NET](https://playwright.dev/dotnet/)

## Getting Started

### Prerequisites

- [.Net 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repo
   ```pwsh
   git clone
   ```
2. Build the solution
   ```pwsh
   dotnet build
   ```
### Host the site locally
1. From the command line, run the following...
   ```pwsh
   dotnet run --project src/Timesheets.Web
   ```
2. Alternativly, the site should run from all good .NET IDE's (i.e. Visual Studio and Rider) out the box.
### Run end-to-end tests on locally hosted site

1. If you've never run [Playwright](https://playwright.dev/dotnet/) before, build the solution then run this the first time to install required browsers. [More Info...](https://playwright.dev/dotnet/docs/intro)
   ```pwsh
   tests/Timesheets.Web.Tests/bin/Debug/net8.0/playwright.ps1 install
   ```
2. Run the tests
   ```pwsh
   $env:ASPNETCORE_ENVIRONMENT = "Development"
   dotnet run --project src/Timesheets.Web
   dotnet test tests/Timesheets.Web.Tests
   ```

# DotNetCoreSimpleApi

A minimalist and clean .NET API project following **Clean Architecture principles**, with separation of concerns, testable components, and modular structure.

Some of the features mentioned above are still under construction, so you might not find all implementations (yet).
I truly appreciate your feedback to help me sharpen my developer skills. It means a lot as I work toward the right opportunity in the near future.

## Purpose

This project was designed as part of my technical refresh and to showcase architectural maturity in modern .NET development. It leverages best practices like SOLID, extension-based configuration, and domain isolation.

## Tech Stack

- ASP.NET Core 8+
- Clean Architecture
- Swagger (Swashbuckle)
- xUnit for Unit Testing
- AutoMapper
- Entity Framework Core (optional)
- Docker (optional)


Each layer has a clear responsibility to ensure maintainability and scalability.

## Features

- Modular service registration using extension methods  
- DTOs and use cases aligned with business rules  
- Unit tests with clear separation by responsibility  
- Prepped for CI/CD and containerization

## How to Run

```bash
dotnet build
dotnet run --project src/API

docker build -t dotnet-core-api .
docker run -p 5000:80 dotnet-core-api
```

## Project Structure
/Src
  /DotNet.Core.Simple.API.Domain => Domain core (Entity, Exception, Validation...)
  
/Tests
  /DotNet.Core.Simple.API.UnitTests => Unit test covering each layer
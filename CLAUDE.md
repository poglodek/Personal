# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Development Commands

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL (via Docker or local installation)

### Restore Dependencies
```bash
cd server
dotnet restore
```

### Build
```bash
cd server
dotnet build Personal.sln -c Release -nologo
```

### Run the API
```bash
cd server/Personal.API
dotnet run
```

### Database
Start PostgreSQL via Docker Compose:
```bash
cd server
docker-compose up -d
```

Connection string configuration is managed per module (see Module Configuration below).

### Running Tests
```bash
cd server
dotnet test -v minimal
```

To run a specific test:
```bash
dotnet test --filter "FullyQualifiedName~Namespace.ClassName.MethodName"
```

To run all tests in a class:
```bash
dotnet test --filter "FullyQualifiedName~Namespace.ClassName"
```

## High-Level Architecture

This is a **modular monolith** ASP.NET Core application using a vertical slice architecture with clear separation between modules.

### Solution Structure

```
server/
├── Personal.API/              # Main API entry point
├── Framework/                 # Cross-cutting infrastructure
│   ├── ApiShared/            # Module system & base middleware
│   ├── Auth/                 # JWT authentication framework
│   ├── Dal.Postgres/         # Database abstractions & Unit of Work
│   ├── Messaging.Integration/# Integration event system
│   └── Shared/               # Domain primitives & events
└── Modules/                  # Business modules (vertical slices)
    ├── User/
    ├── Ward/
    ├── Workout/
    └── Notification/
```

### Module System

Each module is a self-contained vertical slice with:
- **Domain layer**: Entities, value objects, domain events
- **Application layer**: Commands, queries, handlers, DTOs, repositories (interfaces)
- **Infrastructure layer**: EF Core DbContext, repository implementations, queries
- **Shared layer**: Public API exposed to Personal.API, implements `IModule` interface

Modules are **dynamically loaded** at startup:
1. Program.cs calls `builder.InstallModules(typeof(UserModule), typeof(WardModule), ...)`
2. Each module provides a `module.{ModuleName}.json` config file with `Enabled: true/false`
3. The `IModule` interface defines:
   - `InstallModule(IServiceCollection)` - register dependencies
   - `AddEndPoints(IEndpointRouteBuilder)` - register minimal API endpoints
   - `InstallModule(IServiceProvider)` - run migrations/seed data
4. All endpoints are automatically prefixed with `api/{ModuleName}`

### Domain-Driven Design Patterns

**Entity Base Class** (`Shared.Core.Entity`):
- All domain entities inherit from this
- Provides `Id: Guid` and domain event collection
- `RaiseUp(IDomainEvent)` method to capture events during entity operations

**Domain Events → Application Events**:
1. Domain entities raise domain events (e.g., `UserCreated : IDomainEvent`)
2. Events are stored in the entity's internal list
3. `DomainApplicationEventMapper<TDomainEvent>` automatically maps domain events to application events
4. Application event handlers (e.g., `UserCreatedHandler`) react to publish integration events

**Unit of Work Pattern**:
- `IUnitOfWork` interface in `Dal.Postgres`
- `UnitOfWorkPipeline` MediatR behavior automatically commits transactions after command handlers
- Domain events are published within the same transaction boundary

### Authentication & Authorization

JWT-based authentication configured in `Framework/Auth`:
- Supports both symmetric key (JwtKey) and certificate-based signing
- Configuration via `appsettings.json` → `auth` section
- `HttpContext.GetUserId()` extension method extracts user ID from JWT claims
- Endpoints use `.RequireAuthorization()` or `.AllowAnonymous()`

### Value Objects

Modules use strongly-typed value objects to enforce domain rules:
- `Mail` - validates email format
- `PhoneNumber` - validates phone format
- `Password` - encapsulates password hashing/validation
- `Name`, `Address`, `Role`, `Claim` - self-validating domain concepts

These throw domain exceptions (e.g., `MailNotValid`) when invariants are violated.

### MediatR Command/Query Pattern

All business operations use MediatR:
- **Commands**: Modify state (e.g., `CreateUserRequestCommand`)
- **Queries**: Read-only operations (e.g., `GetByIdRequest`)
- Handlers are registered per module via `AddMediatR(cfg => cfg.RegisterServicesFromAssembly(...))`

### Integration Events

Cross-module communication uses integration events (`IIntegrationEvent`):
- Processed by `IntegrationProcessor`
- Enables loose coupling between modules
- Handlers registered via `Messaging.Integration` framework

## Key Files & Patterns

### Adding a New Module
1. Create folder structure: `Modules/{ModuleName}/{ModuleName}.Domain|Application|Infrastructure|Shared`
2. Create `{ModuleName}Module.cs` in Shared layer implementing `IModule`
3. Add `module.{ModuleName}.json` with `{ "Enabled": true }` to Shared layer
4. Register in `Program.cs`: `builder.InstallModules(..., typeof(YourModule))`
5. Define EF Core DbContext in Infrastructure, add migration
6. Implement `UseInfra()` extension to run migrations

### Adding an Endpoint to Existing Module
1. Navigate to `Modules/{Module}/{Module}.Shared/{Module}Module.cs`
2. Add endpoint in `AddEndPoints(IEndpointRouteBuilder)` method
3. Endpoint paths are automatically prefixed with `api/{ModuleName}`

### Creating a Command/Query
1. Define command/query record in `{Module}.Application/Command|Query/{Operation}/`
2. Create handler implementing `IRequestHandler<TRequest, TResponse>`
3. Inject `IUnitOfWork` for commands, repositories for queries
4. Domain events raised in entities are automatically published after SaveChanges

## Configuration Notes

- Target framework: **net10.0** (API project) / **net8.0** (some Framework projects - check .csproj)
- Nullable reference types enabled
- Module configuration files must exist at runtime (copied to output via .csproj settings)
- Database connection strings configured per module in their respective `module.{Name}.json` or appsettings

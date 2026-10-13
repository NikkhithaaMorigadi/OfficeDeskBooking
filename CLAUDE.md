# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

OfficeDeskBooking is an ASP.NET Core 8 Web API for booking office desk seats. Solution file: `OfficeDeskBooking.sln`, single project: `OfficeDeskBooking/OfficeDeskBooking.csproj` (assembly/root namespace `OfficeDeskBooking.API`, though some files under `Models`/`DTOs`/`Interfaces` use the bare `OfficeDeskBooking` namespace — watch for this mismatch when adding `using` statements). No frontend is in this repo; CORS is configured for a React app at `http://localhost:3000`. There is no test project in the solution.

## Commands

Run all commands from `OfficeDeskBooking/` (the folder containing the `.csproj`), or pass `OfficeDeskBooking.sln` explicitly from the repo root.

- Build: `dotnet build`
- Run (dev, with Swagger UI): `dotnet run` — launch profiles are in `Properties/launchSettings.json`
- Add an EF Core migration: `dotnet ef migrations add <Name>`
- Apply migrations to the database: `dotnet ef database update`

There are no test projects or lint tooling configured in this repo.

## Architecture

Standard layered structure, each layer depending only on the one below via interfaces in `Interfaces/`:

- **Controllers** (`Controllers/`) — thin ASP.NET Core controllers that call into a service and wrap the result in `Ok(...)`. No controller-level validation or error handling beyond what the service/repository returns.
- **Services** (`Services/`) — currently pass-through wrappers around the matching repository (`TableService` → `ITableRepository`, `BookingService` → `IBookingRepository`); business logic has not been pushed down from repositories into this layer yet.
- **Repositories** (`Repositories/`) — own all EF Core query/persistence logic against `OfficeDbContext`.
- **Data** (`Data/OfficeDbContext.cs`) — EF Core `DbContext` with `DbSet`s for `Table`, `Seat`, `Booking`, `User`. Provider is SQL Server (`UseSqlServer`), configured in `Program.cs` from the `DefaultConnection` connection string.
- **Models** (`Models/`) vs **DTOs** (`DTOs/`) — Models are EF entities; DTOs are the shapes returned to/from controllers. `TableRepository.GetTablesAsync` is the main place entities get projected into DTOs (`TableDto`/`SeatDto`), including computing `IsBooked`/`EmployeeName` per seat from the `Bookings` table for a given date.
- **Migrations** (`Migrations/`) — EF Core migrations; `OfficeDbContextModelSnapshot.cs` reflects current schema state.

Auth: JWT bearer auth is wired up in `Program.cs` via `AddJwtBearer`, using `Jwt:Key`/`Jwt:Issuer`/`Jwt:Audience`/`Jwt:ExpiryMinutes` from configuration (see `appsettings.json`). `AuthController` issues tokens on `/api/auth/login` and creates users on `/api/auth/register` by comparing/storing `Password` as plain text (no hashing) — this is called out in-code with `// Hash in production`. `JwtService.GenerateToken` embeds `EmployeeName`, `Email`, `Role`, `EmployeeId`, `UserId` as claims. Only `TablesController` currently has `[Authorize]`; `BookingsController` does not.

Booking flow: `BookingsController.BookSeat` → `BookingService.BookSeatAsync` → `BookingRepository.BookSeatAsync`, which loops over `BookingRequestDto.SeatIds`, adds a `Booking` per seat, and calls `SaveChangesAsync()` once for the batch.

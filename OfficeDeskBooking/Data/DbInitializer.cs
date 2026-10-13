// ============================================================================
// File    : DbInitializer.cs
// Layer   : Infrastructure — Data
// Author  : [Author Name]  │  Created: 2026-10-13
// ============================================================================
// Summary : Seeds the default office layout (4 tables per room, 4 seats per
//           table) on startup when the Tables table is empty. Rooms 1 and 2
//           themselves are seeded by the AddRoomModel migration.
// ============================================================================
using Microsoft.EntityFrameworkCore;
using OfficeDeskBooking.API.Models;

namespace OfficeDeskBooking.API.Data;

public static class DbInitializer
{
    private const int TABLES_PER_ROOM = 4;
    private const int SEATS_PER_TABLE = 4;
    private static readonly int[] ROOM_IDS = { 1, 2 };

    /// <summary>
    /// Adds the default tables and seats when no tables exist. Does nothing while
    /// migrations are pending, so it is safe to run before <c>dotnet ef database update</c>.
    /// </summary>
    public static async Task SeedAsync(OfficeDbContext context, CancellationToken cancellationToken = default)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            return;
        }

        if (await context.Tables.AnyAsync(cancellationToken))
        {
            return;
        }

        foreach (var roomId in ROOM_IDS)
        {
            for (var tableNumber = 1; tableNumber <= TABLES_PER_ROOM; tableNumber++)
            {
                var table = new Table
                {
                    TableName = $"Table {tableNumber}",
                    Floor = 1,
                    TotalSeats = SEATS_PER_TABLE,
                    RoomId = roomId
                };

                for (var seatNumber = 1; seatNumber <= SEATS_PER_TABLE; seatNumber++)
                {
                    table.Seats.Add(new Seat { SeatNumber = seatNumber, IsBooked = false });
                }

                context.Tables.Add(table);
            }
        }

        // One SaveChanges for the whole layout (not inside the loops).
        await context.SaveChangesAsync(cancellationToken);
    }
}

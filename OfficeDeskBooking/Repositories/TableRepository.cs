using Microsoft.EntityFrameworkCore;
using OfficeDeskBooking.API.Data;
using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;

namespace OfficeDeskBooking.API.Repositories;

public class TableRepository : ITableRepository
{
    private readonly OfficeDbContext _context;

    public TableRepository(OfficeDbContext context)
    {
        _context = context;
    }
//getting table data
    public async Task<List<TableDto>> GetTablesAsync(DateTime bookingDate)
    {
        var bookedSeats = await _context.Bookings
    .Where(b => b.BookingDate.Date == bookingDate.Date)
    .Select(b => new
    {
        b.SeatId,
        b.EmployeeName
    })
    .ToDictionaryAsync(b => b.SeatId, b => b.EmployeeName);

        var value=await   _context.Tables
            .Include(t => t.Seats)
            .Select(t => new TableDto
            {
                TableId = t.TableId,
                TableName = t.TableName,
                Floor = t.Floor,
                RoomId = t.RoomId,
                RoomName = t.Room.RoomName,
                Seats = t.Seats
                    .OrderBy(s => s.SeatNumber)
                    .Select(s => new SeatDto
                    {
                        SeatId = s.SeatId,
                        SeatNumber = s.SeatNumber,
                        IsBooked = bookedSeats.ContainsKey(s.SeatId),
                        EmployeeName = bookedSeats.ContainsKey(s.SeatId)
                    ? bookedSeats[s.SeatId]
                    : null
                    }).ToList()
            }).ToListAsync();
        return value;
    }
}
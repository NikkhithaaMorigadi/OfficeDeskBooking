using OfficeDeskBooking.API.Data;
using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;
using OfficeDeskBooking.API.Models;

namespace OfficeDeskBooking.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly OfficeDbContext _context;

    public BookingRepository(OfficeDbContext context)
    {
        _context = context;
    }

    public async Task<string> BookSeatAsync(BookingRequestDto request)
    {
        try
        {
            foreach (var seatId in request.SeatIds)
            {
                var seat = await _context.Seats.FindAsync(seatId);


                var booking = new Booking
                {
                    SeatId = seatId,
                    EmployeeName = request.EmployeeName,
                    BookingDate = request.BookingDate,

                };


                _context.Bookings.Add(booking);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

        }
        return "Seat booked successfully";
    }
}
using OfficeDeskBooking.API.DTOs;

namespace OfficeDeskBooking.API.Interfaces;

public interface IBookingService
{
    Task<string> BookSeatAsync(BookingRequestDto request);
}

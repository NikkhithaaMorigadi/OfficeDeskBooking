using OfficeDeskBooking.API.DTOs;

namespace OfficeDeskBooking.API.Interfaces;

public interface IBookingRepository
{
    Task<string> BookSeatAsync(BookingRequestDto request);
}
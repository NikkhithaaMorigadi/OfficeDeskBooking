using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;

namespace OfficeDeskBooking.API.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public Task<string> BookSeatAsync(BookingRequestDto request)
    {
        return _repository.BookSeatAsync(request);
    }
}
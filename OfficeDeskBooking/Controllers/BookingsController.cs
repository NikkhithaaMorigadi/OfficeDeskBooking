using Microsoft.AspNetCore.Mvc;
using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;

namespace OfficeDeskBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingsController(IBookingService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> BookSeat(BookingRequestDto request)
    {
        var result = await _service.BookSeatAsync(request);
        return Ok(result);
    }
}
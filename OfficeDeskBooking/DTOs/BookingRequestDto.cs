namespace OfficeDeskBooking.API.DTOs;

public class BookingRequestDto
{
    public List<int> SeatIds { get; set; } = new();

    public string EmployeeName { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }
}
namespace OfficeDeskBooking.API.DTOs;

public class SeatDto
{
    public int SeatId { get; set; }
    public int SeatNumber { get; set; }
    public bool IsBooked { get; set; }
    public string? EmployeeName { get; set; }
}
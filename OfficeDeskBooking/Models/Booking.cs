namespace OfficeDeskBooking.API.Models;

public class Booking
{
    public int BookingId { get; set; }

    public int SeatId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public Seat Seat { get; set; } = null!;
}
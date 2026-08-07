namespace OfficeDeskBooking.API.Models;

public class Seat
{
    public int SeatId { get; set; }

    public int TableId { get; set; }

    public int SeatNumber { get; set; }

    public bool IsBooked { get; set; }

    public Table Table { get; set; } = null!;
}
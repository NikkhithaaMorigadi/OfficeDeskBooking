namespace OfficeDeskBooking.API.Models;

public class Table
{
    public int TableId { get; set; }

    public string TableName { get; set; } = string.Empty;

    public int Floor { get; set; }

    public int TotalSeats { get; set; }

    public int RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
namespace OfficeDeskBooking.API.DTOs;

public class TableDto
{
    public int TableId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public int Floor { get; set; }
    
    public List<SeatDto> Seats { get; set; } = new();
}
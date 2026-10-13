// ============================================================================
// File    : Room.cs
// Layer   : Domain — Models
// Author  : [Author Name]  │  Created: 2026-10-13
// ============================================================================
// Summary : Office room entity; each room groups the tables (and their seats)
//           that employees can book.
// ============================================================================
namespace OfficeDeskBooking.API.Models;

public class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public ICollection<Table> Tables { get; set; } = new List<Table>();
}

using OfficeDeskBooking.API.DTOs;

namespace OfficeDeskBooking.API.Interfaces;

public interface ITableRepository
{
    Task<List<TableDto>> GetTablesAsync(DateTime bookingDate);
}
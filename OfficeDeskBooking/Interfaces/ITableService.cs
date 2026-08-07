using OfficeDeskBooking.API.DTOs;

namespace OfficeDeskBooking.API.Interfaces;

public interface ITableService
{
    Task<List<TableDto>> GetTablesAsync(DateTime bookingDate);
}
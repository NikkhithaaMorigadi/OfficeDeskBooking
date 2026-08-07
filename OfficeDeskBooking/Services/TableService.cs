using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;

namespace OfficeDeskBooking.API.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _repository;

    public TableService(ITableRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TableDto>> GetTablesAsync(DateTime bookingDate)
    {
        return _repository.GetTablesAsync(bookingDate);
    }
}
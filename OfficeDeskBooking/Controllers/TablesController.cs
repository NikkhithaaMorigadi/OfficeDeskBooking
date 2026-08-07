using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeDeskBooking.API.Interfaces;

namespace OfficeDeskBooking.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly ITableService _service;

    public TablesController(ITableService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTables(DateTime bookingDate)
    {
        var data = await _service.GetTablesAsync(bookingDate);
        return Ok(data);
    }
}
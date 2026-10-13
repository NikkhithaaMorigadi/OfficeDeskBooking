using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeDeskBooking.API.Data;
using OfficeDeskBooking.API.DTOs;
using OfficeDeskBooking.API.Interfaces;
using OfficeDeskBooking.DTOs;
using OfficeDeskBooking.Interfaces;
using OfficeDeskBooking.Models;

namespace OfficeDeskBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly OfficeDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(
        OfficeDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x =>
            x.UserEmail == request.Email &&
            x.Password == request.Password);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var token = _jwtService.GenerateToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName,
            Role = user.Role
        });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.UserEmail == request.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists.");
        }

        var user = new User
        {
            UserName = request.UserName,
            UserEmail = request.Email,
            Password = request.Password // Hash in production
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok("Registration Successful");
    }
}
using Microsoft.EntityFrameworkCore;
using OfficeDeskBooking.API.Models;
using OfficeDeskBooking.Models;

namespace OfficeDeskBooking.API.Data;

public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Table> Tables => Set<Table>();

    public DbSet<Seat> Seats => Set<Seat>();

    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<User> Users => Set<User>();
}
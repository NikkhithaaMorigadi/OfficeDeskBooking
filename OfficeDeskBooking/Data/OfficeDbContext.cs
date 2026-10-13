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

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Table> Tables => Set<Table>();

    public DbSet<Seat> Seats => Set<Seat>();

    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>(room =>
        {
            room.Property(r => r.RoomName).IsRequired().HasMaxLength(100);

            room.HasData(
                new Room { RoomId = 1, RoomName = "Room 1" },
                new Room { RoomId = 2, RoomName = "Room 2" });
        });

        modelBuilder.Entity<Table>(table =>
        {
            // Default of 1 lets existing tables point at Room 1 when the column is added.
            table.Property(t => t.RoomId).HasDefaultValue(1);

            table.HasOne(t => t.Room)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
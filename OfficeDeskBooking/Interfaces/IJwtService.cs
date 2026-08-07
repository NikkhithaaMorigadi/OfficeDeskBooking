using OfficeDeskBooking.Models;

namespace OfficeDeskBooking.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}

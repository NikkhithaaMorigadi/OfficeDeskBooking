namespace OfficeDeskBooking.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string EmployeeId { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Employee";
    }
}

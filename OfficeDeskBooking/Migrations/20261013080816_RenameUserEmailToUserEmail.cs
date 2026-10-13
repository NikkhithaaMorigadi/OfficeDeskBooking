using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeDeskBooking.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserEmailToUserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Users",
                newName: "Email");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OfficeDeskBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "RoomName" },
                values: new object[,]
                {
                    { 1, "Room 1" },
                    { 2, "Room 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RoomId",
                table: "Tables",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Rooms_RoomId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Tables_RoomId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Tables");
        }
    }
}

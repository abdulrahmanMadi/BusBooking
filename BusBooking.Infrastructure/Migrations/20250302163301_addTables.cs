using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "EmailId", "FullName", "Password", "ProjectName", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 2, 16, 33, 1, 158, DateTimeKind.Utc).AddTicks(3967), "admin@example.com", "Admin User", "admin123", "BusBooking", null, null, "Admin", "admin" },
                    { 2, new DateTime(2025, 3, 2, 16, 33, 1, 158, DateTimeKind.Utc).AddTicks(3974), "customer@example.com", "Customer User", "customer123", "BusBooking", null, null, "Customer", "customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

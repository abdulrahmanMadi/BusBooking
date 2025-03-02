using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 2, 16, 33, 48, 688, DateTimeKind.Utc).AddTicks(6751));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 2, 16, 33, 48, 688, DateTimeKind.Utc).AddTicks(6757));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 2, 16, 33, 1, 158, DateTimeKind.Utc).AddTicks(3967));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 2, 16, 33, 1, 158, DateTimeKind.Utc).AddTicks(3974));
        }
    }
}

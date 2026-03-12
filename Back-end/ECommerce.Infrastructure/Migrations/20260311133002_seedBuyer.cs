using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedBuyer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "buyer@buyer.com", "Buyer", "buyer123", "Buyer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}

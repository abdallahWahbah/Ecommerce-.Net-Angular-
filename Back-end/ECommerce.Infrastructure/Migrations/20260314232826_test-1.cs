using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Electronics" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Clothes" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Books" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "admin@admin.com", "Admin", "admin123", "Admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "buyer@buyer.com", "Buyer", "buyer123", "Buyer" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000035"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Comfortable cotton casual shirt", "/images/products/menCasualShirt.jpeg", "Men Casual Shirt", 40m, 120 },
                    { new Guid("10000000-0000-0000-0000-000000000036"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Stylish leather handbag", "/images/products/leatherHandbag.jpeg", "Women Handbag", 85m, 70 },
                    { new Guid("10000000-0000-0000-0000-000000000037"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Classic brown leather belt", "/images/products/brownLeatherBelt.jpeg", "Men Leather Belt", 30m, 150 },
                    { new Guid("10000000-0000-0000-0000-000000000038"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Warm winter jacket for cold weather", "/images/products/winterJacket.jpeg", "Men Winter Jacket", 180m, 45 },
                    { new Guid("10000000-0000-0000-0000-000000000039"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Soft knitted cardigan", "/images/products/knittedCardigan.jpeg", "Women Cardigan", 65m, 90 },
                    { new Guid("10000000-0000-0000-0000-000000000045"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Guide to building maintainable software architecture by Robert C. Martin", "/images/products/CleanArchitecture.jpeg", "Clean Architecture", 55m, 70 },
                    { new Guid("10000000-0000-0000-0000-000000000046"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Best practices and tips for professional programmers", "/images/products/PragmaticProgrammer.jpeg", "The Pragmatic Programmer", 60m, 50 },
                    { new Guid("10000000-0000-0000-0000-000000000047"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Comprehensive guide to algorithms and data structures", "/images/products/algorithms-book.jpeg", "Introduction to Algorithms", 90m, 40 },
                    { new Guid("10000000-0000-0000-0000-000000000048"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Modern systems design and distributed data systems", "/images/products/data-intensive.png", "Designing Data-Intensive Applications", 75m, 35 },
                    { new Guid("10000000-0000-0000-0000-000000000049"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Beginner friendly explanation of software design patterns", "/images/products/head-first-design-patterns.jpeg", "Head First Design Patterns", 65m, 60 },
                    { new Guid("10000000-0000-0000-0000-000000000101"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Keyboard for beginners", "/images/products/laptop.jpeg", "Laptop", 25m, 100 },
                    { new Guid("10000000-0000-0000-0000-000000000102"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Magic mouse", "/images/products/mouse.jpeg", "Mouse", 80m, 50 },
                    { new Guid("10000000-0000-0000-0000-000000000103"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "55 inch UHD TV", "/images/products/monitor.jpeg", "Samsung 4K Smart TV", 900m, 25 },
                    { new Guid("10000000-0000-0000-0000-000000000104"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Noise cancelling headphones", "/images/products/airpods.jpeg", "Sony Headphones", 250m, 60 },
                    { new Guid("10000000-0000-0000-0000-000000000105"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "RGB gaming keyboard", "/images/products/keyboard.jpeg", "Mechanical Keyboard", 150m, 70 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

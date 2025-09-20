using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanCode.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // criar dados para a tabela Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: ["Id", "Name", "Description", "Price", "Stock", "Image", "CategoryId"],
                values: new object[,]
                {
                    { 1, "Smartphone", "Latest model smartphone with advanced features", 699.99m, 50, "smartphone.jpg", 1 },
                    { 2, "Laptop", "High-performance laptop for work and gaming", 1299.99m, 30, "laptop.jpg", 1 },
                    { 3, "Headphones", "Noise-cancelling over-ear headphones", 199.99m, 100, "headphones.jpg", 1 },
                    { 4, "Science Fiction Novel", "A thrilling science fiction adventure", 14.99m, 200, "scifi_novel.jpg", 2 },
                    { 5, "Mystery Novel", "A gripping mystery novel full of twists", 12.99m, 150, "mystery_novel.jpg", 2 },
                    { 6, "Fantasy Novel", "An epic fantasy tale of magic and heroism", 16.99m, 180, "fantasy_novel.jpg", 2 },
                    { 7, "T-Shirt", "Comfortable cotton t-shirt in various sizes", 19.99m, 300, "tshirt.jpg", 3 },
                    { 8, "Jeans", "Stylish denim jeans with a modern fit", 49.99m, 120, "jeans.jpg", 3 },
                    { 9, "Jacket", "Warm and durable jacket for all seasons", 89.99m, 80, "jacket.jpg", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: [ 1, 2, 3, 4, 5, 6, 7, 8, 9 ]);
        }
    }
}

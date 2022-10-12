using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    public partial class PopulateProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Diet Coke', 'Coke Soda 350 ml', 5.45, 'coke.jpg', 50, GETDATE(), 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Tuna Snack', 'Tuna snack with mayonnaise', 8.50, 'tuna-snack.jpg', 10, GETDATE(), 2)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Pudding', 'Milk Pudding 100g', 6.75, 'pudding.jpg', 20, GETDATE(), 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}

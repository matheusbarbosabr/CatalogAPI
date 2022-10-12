using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    public partial class PopulateCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Drinks', 'drinks.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Snacks', 'snacks.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES ('Desserts', 'desserts.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}

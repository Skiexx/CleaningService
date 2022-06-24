using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleaningService.Migrations
{
    public partial class AddKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint("User_UN", "Users", "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint("User_UN", "Users");
        }
    }
}

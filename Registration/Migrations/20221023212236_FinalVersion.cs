using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.Migrations
{
    public partial class FinalVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorUser",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorUser",
                table: "Blogs");
        }
    }
}

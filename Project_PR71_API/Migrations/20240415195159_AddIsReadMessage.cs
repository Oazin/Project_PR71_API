using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_PR71_API.Migrations
{
    public partial class AddIsReadMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Message",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Message");
        }
    }
}

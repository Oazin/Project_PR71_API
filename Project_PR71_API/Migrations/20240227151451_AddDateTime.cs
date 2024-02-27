using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_PR71_API.Migrations
{
    public partial class AddDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_Useremail",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Fistname",
                table: "User",
                newName: "Firstname");

            migrationBuilder.RenameIndex(
                name: "IX_User_email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameColumn(
                name: "Useremail",
                table: "Post",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Post_Useremail",
                table: "Post",
                newName: "IX_Post_UserEmail");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UserEmail",
                table: "Post",
                column: "UserEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UserEmail",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "User",
                newName: "Fistname");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "User",
                newName: "IX_User_email");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Post",
                newName: "Useremail");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UserEmail",
                table: "Post",
                newName: "IX_Post_Useremail");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_Useremail",
                table: "Post",
                column: "Useremail",
                principalTable: "User",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

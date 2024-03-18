using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_PR71_API.Migrations
{
    public partial class AddModelChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ReceiverEmail",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ReceiverEmail",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User1Email = table.Column<string>(type: "text", nullable: false),
                    User2Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_User_User1Email",
                        column: x => x.User1Email,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chat_User_User2Email",
                        column: x => x.User2Email,
                        principalTable: "User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_Id",
                table: "Chat",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User1Email",
                table: "Chat",
                column: "User1Email");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User2Email",
                table: "Chat",
                column: "User2Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Chat_ChatId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverEmail",
                table: "Message",
                column: "ReceiverEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ReceiverEmail",
                table: "Message",
                column: "ReceiverEmail",
                principalTable: "User",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

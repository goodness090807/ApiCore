using Microsoft.EntityFrameworkCore.Migrations;

namespace APICore.Migrations
{
    public partial class addmanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "TodoLists",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "listType",
                table: "TodoLists",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "TodoLists",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInfouserId",
                table: "TodoLists",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersLists",
                columns: table => new
                {
                    userId = table.Column<string>(nullable: false),
                    listId = table.Column<string>(nullable: false),
                    UserInfouserId = table.Column<string>(nullable: true),
                    TodoListlistId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLists", x => new { x.userId, x.listId });
                    table.ForeignKey(
                        name: "FK_UsersLists_TodoLists_TodoListlistId",
                        column: x => x.TodoListlistId,
                        principalTable: "TodoLists",
                        principalColumn: "listId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersLists_UserInfos_UserInfouserId",
                        column: x => x.UserInfouserId,
                        principalTable: "UserInfos",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoLists_UserInfouserId",
                table: "TodoLists",
                column: "UserInfouserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLists_TodoListlistId",
                table: "UsersLists",
                column: "TodoListlistId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLists_UserInfouserId",
                table: "UsersLists",
                column: "UserInfouserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoLists_UserInfos_UserInfouserId",
                table: "TodoLists",
                column: "UserInfouserId",
                principalTable: "UserInfos",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoLists_UserInfos_UserInfouserId",
                table: "TodoLists");

            migrationBuilder.DropTable(
                name: "UsersLists");

            migrationBuilder.DropIndex(
                name: "IX_TodoLists_UserInfouserId",
                table: "TodoLists");

            migrationBuilder.DropColumn(
                name: "UserInfouserId",
                table: "TodoLists");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "TodoLists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "listType",
                table: "TodoLists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "TodoLists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000);
        }
    }
}

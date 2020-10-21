using Microsoft.EntityFrameworkCore.Migrations;

namespace APICore.Migrations
{
    public partial class setRolesRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userRoles",
                table: "UserInfos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userRoles",
                table: "UserInfos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

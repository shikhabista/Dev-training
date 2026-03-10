using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet_Mvc.Migrations
{
    /// <inheritdoc />
    public partial class UserModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "admin",
                table: "user",
                newName: "username");

            migrationBuilder.AlterColumn<string>(
                name: "rec_status",
                schema: "admin",
                table: "user",
                type: "text",
                nullable: false,
                oldClrType: typeof(char),
                oldType: "character(1)");

            migrationBuilder.AddColumn<string>(
                name: "contact_no",
                schema: "admin",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "admin",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact_no",
                schema: "admin",
                table: "user");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "admin",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "admin",
                table: "user",
                newName: "user_name");

            migrationBuilder.AlterColumn<char>(
                name: "rec_status",
                schema: "admin",
                table: "user",
                type: "character(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

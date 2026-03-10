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
        }
    }
}

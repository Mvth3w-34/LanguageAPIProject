using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguageProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class chNewWordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Arabic",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "French",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Swahili",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arabic",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "French",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Swahili",
                table: "Words");
        }
    }
}

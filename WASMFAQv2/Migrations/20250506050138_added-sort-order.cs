using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WASMFAQv2.Migrations
{
    /// <inheritdoc />
    public partial class Addedsortorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "QnASets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "QnAs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "QnASets");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "QnAs");
        }
    }
}

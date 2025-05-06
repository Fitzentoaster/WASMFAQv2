using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WASMFAQv2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QnASets",
                columns: table => new
                {
                    QnASetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QnASets", x => x.QnASetId);
                });

            migrationBuilder.CreateTable(
                name: "QnAs",
                columns: table => new
                {
                    QnaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QnASetId = table.Column<int>(type: "integer", nullable: true),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QnAs", x => x.QnaId);
                    table.ForeignKey(
                        name: "FK_QnAs_QnASets_QnASetId",
                        column: x => x.QnASetId,
                        principalTable: "QnASets",
                        principalColumn: "QnASetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QnAs_QnASetId",
                table: "QnAs",
                column: "QnASetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QnAs");

            migrationBuilder.DropTable(
                name: "QnASets");
        }
    }
}

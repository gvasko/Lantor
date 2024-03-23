using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersistVectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alphabets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alphabets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageVectorCache",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageSampleId = table.Column<int>(type: "int", nullable: false),
                    AlphabetId = table.Column<int>(type: "int", nullable: false),
                    Vector_Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageVectorCache", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageVectorCache_Alphabets_AlphabetId",
                        column: x => x.AlphabetId,
                        principalTable: "Alphabets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageVectorCache_LanguageSamples_LanguageSampleId",
                        column: x => x.LanguageSampleId,
                        principalTable: "LanguageSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LetterVector",
                columns: table => new
                {
                    AlphabetId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letter = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Vector_Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterVector", x => new { x.AlphabetId, x.Id });
                    table.ForeignKey(
                        name: "FK_LetterVector_Alphabets_AlphabetId",
                        column: x => x.AlphabetId,
                        principalTable: "Alphabets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageVectorCache_AlphabetId",
                table: "LanguageVectorCache",
                column: "AlphabetId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageVectorCache_LanguageSampleId",
                table: "LanguageVectorCache",
                column: "LanguageSampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageVectorCache");

            migrationBuilder.DropTable(
                name: "LetterVector");

            migrationBuilder.DropTable(
                name: "Alphabets");
        }
    }
}

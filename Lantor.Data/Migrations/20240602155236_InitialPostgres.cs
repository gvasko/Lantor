using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alphabets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alphabets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultilingualSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultilingualSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterVector",
                columns: table => new
                {
                    AlphabetId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Letter = table.Column<char>(type: "character(1)", nullable: false),
                    Vector_Data = table.Column<byte[]>(type: "bytea", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LanguageSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Sample = table.Column<string>(type: "text", nullable: false),
                    MultilingualSampleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                        column: x => x.MultilingualSampleId,
                        principalTable: "MultilingualSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageVectorCache",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageSampleId = table.Column<int>(type: "integer", nullable: false),
                    AlphabetId = table.Column<int>(type: "integer", nullable: false),
                    Vector_Data = table.Column<byte[]>(type: "bytea", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSamples_MultilingualSampleId",
                table: "LanguageSamples",
                column: "MultilingualSampleId");

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
                name: "LanguageSamples");

            migrationBuilder.DropTable(
                name: "Alphabets");

            migrationBuilder.DropTable(
                name: "MultilingualSamples");
        }
    }
}

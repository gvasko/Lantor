using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lantor");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "lantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alphabets",
                schema: "lantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alphabets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alphabets_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "lantor",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultilingualSamples",
                schema: "lantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultilingualSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultilingualSamples_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "lantor",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LetterVector",
                schema: "lantor",
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
                        principalSchema: "lantor",
                        principalTable: "Alphabets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSamples",
                schema: "lantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultilingualSampleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                        column: x => x.MultilingualSampleId,
                        principalSchema: "lantor",
                        principalTable: "MultilingualSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageVectorCache",
                schema: "lantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageSampleId = table.Column<int>(type: "int", nullable: true),
                    AlphabetId = table.Column<int>(type: "int", nullable: true),
                    Vector_Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageVectorCache", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageVectorCache_Alphabets_AlphabetId",
                        column: x => x.AlphabetId,
                        principalSchema: "lantor",
                        principalTable: "Alphabets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LanguageVectorCache_LanguageSamples_LanguageSampleId",
                        column: x => x.LanguageSampleId,
                        principalSchema: "lantor",
                        principalTable: "LanguageSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alphabets_OwnerId",
                schema: "lantor",
                table: "Alphabets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSamples_MultilingualSampleId",
                schema: "lantor",
                table: "LanguageSamples",
                column: "MultilingualSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageVectorCache_AlphabetId",
                schema: "lantor",
                table: "LanguageVectorCache",
                column: "AlphabetId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageVectorCache_LanguageSampleId",
                schema: "lantor",
                table: "LanguageVectorCache",
                column: "LanguageSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_MultilingualSamples_OwnerId",
                schema: "lantor",
                table: "MultilingualSamples",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageVectorCache",
                schema: "lantor");

            migrationBuilder.DropTable(
                name: "LetterVector",
                schema: "lantor");

            migrationBuilder.DropTable(
                name: "LanguageSamples",
                schema: "lantor");

            migrationBuilder.DropTable(
                name: "Alphabets",
                schema: "lantor");

            migrationBuilder.DropTable(
                name: "MultilingualSamples",
                schema: "lantor");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "lantor");
        }
    }
}

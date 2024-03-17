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
            migrationBuilder.CreateTable(
                name: "MultilingualSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultilingualSamples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultilingualSampleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                        column: x => x.MultilingualSampleId,
                        principalTable: "MultilingualSamples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSamples_MultilingualSampleId",
                table: "LanguageSamples",
                column: "MultilingualSampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageSamples");

            migrationBuilder.DropTable(
                name: "MultilingualSamples");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguageSampleParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                table: "LanguageSamples");

            migrationBuilder.AlterColumn<int>(
                name: "MultilingualSampleId",
                table: "LanguageSamples",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                table: "LanguageSamples",
                column: "MultilingualSampleId",
                principalTable: "MultilingualSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                table: "LanguageSamples");

            migrationBuilder.AlterColumn<int>(
                name: "MultilingualSampleId",
                table: "LanguageSamples",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageSamples_MultilingualSamples_MultilingualSampleId",
                table: "LanguageSamples",
                column: "MultilingualSampleId",
                principalTable: "MultilingualSamples",
                principalColumn: "Id");
        }
    }
}

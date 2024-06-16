using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "MultilingualSamples",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Alphabets",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_MultilingualSamples_OwnerId",
                table: "MultilingualSamples",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Alphabets_OwnerId",
                table: "Alphabets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alphabets_Users_OwnerId",
                table: "Alphabets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MultilingualSamples_Users_OwnerId",
                table: "MultilingualSamples",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alphabets_Users_OwnerId",
                table: "Alphabets");

            migrationBuilder.DropForeignKey(
                name: "FK_MultilingualSamples_Users_OwnerId",
                table: "MultilingualSamples");

            migrationBuilder.DropIndex(
                name: "IX_MultilingualSamples_OwnerId",
                table: "MultilingualSamples");

            migrationBuilder.DropIndex(
                name: "IX_Alphabets_OwnerId",
                table: "Alphabets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "MultilingualSamples");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Alphabets");
        }
    }
}

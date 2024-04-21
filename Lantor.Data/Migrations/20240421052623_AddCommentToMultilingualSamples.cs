using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentToMultilingualSamples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "MultilingualSamples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "MultilingualSamples");
        }
    }
}

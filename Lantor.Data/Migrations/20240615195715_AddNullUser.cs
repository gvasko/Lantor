using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNullUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "UserName", "Email", "ExternalId" },
                values: new object[,]
                {
                    { 1, "NullUser", "NullUser", "", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

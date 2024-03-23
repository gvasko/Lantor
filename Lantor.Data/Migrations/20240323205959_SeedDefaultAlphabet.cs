using Lantor.DomainModel;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultAlphabet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var defaultAlphabet = new Alphabet("Default", 5024);
            migrationBuilder.InsertData(
                table: "Alphabets",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, defaultAlphabet.Name }
                });

            for (int i = 0; i < defaultAlphabet.LetterVectors.Count; i++)
            {
                var lv = defaultAlphabet.LetterVectors[i];
                var bytes = new byte[lv.Vector.Data.Count / 8];
                lv.Vector.Data.CopyTo(bytes, 0);
                migrationBuilder.InsertData(
                    table: "LetterVector",
                    columns: new[] { "Id", "Letter", "AlphabetId", "Vector_Data" },
                    values: new object[,]
                    {
                        { i+1, lv.Letter.ToString(), 1, bytes }
                    });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("DELETE FROM [LetterVector] WHERE AlphabetId = 1");
            migrationBuilder.DeleteData(
                table: "LetterVector",
                keyColumn: "AlphabetId",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Alphabets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

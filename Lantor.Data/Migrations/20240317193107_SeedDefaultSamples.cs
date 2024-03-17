using Azure.Core;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using System.Text;

#nullable disable

namespace Lantor.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultSamples : Migration
    {
        /// <inheritdoc />
        /// 
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MultilingualSamples",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Default" }
                });

            migrationBuilder.InsertData(
                table: "LanguageSamples",
                columns: new[] { "Id", "Name", "Sample", "MultilingualSampleId" },
                values: new object[,]
                {
                    { 1, "English", ReadEmbeddedTextFile("EnglishSample"), 1 },
                    { 2, "German", ReadEmbeddedTextFile("GermanSample"), 1 },
                    { 3, "French", ReadEmbeddedTextFile("FrenchSample"), 1 },
                    { 4, "Italian", ReadEmbeddedTextFile("ItalianSample"), 1 },
                    { 5, "Spanish", ReadEmbeddedTextFile("SpanishSample"), 1 },
                    { 6, "Portuguese", ReadEmbeddedTextFile("PortugueseSample"), 1 },
                    { 7, "Polish", ReadEmbeddedTextFile("PolishSample"), 1 },
                    { 8, "Dutch", ReadEmbeddedTextFile("DutchSample"), 1 },
                    { 9, "Romanian", ReadEmbeddedTextFile("RomanianSample"), 1 },
                    { 10, "Hungarian", ReadEmbeddedTextFile("HungarianSample"), 1 }
                });
        }

        private static string ReadEmbeddedTextFile(string sampleName)
        {
            string textFileContent = "";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{assembly.GetName().Name}.DefaultData.{sampleName}.txt";
            Console.WriteLine($"Loading embedded resource: {resourceName}");
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new Exception($"Stream is null: {sampleName}");
            }
            else
            {
                using var reader = new StreamReader(stream, Encoding.UTF8);
                textFileContent = reader.ReadToEnd();
            }

            return textFileContent;
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LanguageSamples",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MultilingualSamples",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

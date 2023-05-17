using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class insertNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Asimov'un muhteşem serisi", "Vakıf" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Description", "ImageUrl", "Price", "PublishedDate", "Publisher", "SoftDeleted", "Title" },
                values: new object[] { 2, "Palyaço korkusu yaratan bir kitap", null, null, null, null, false, "O" });

            migrationBuilder.InsertData(
                table: "BookTag",
                columns: new[] { "BooksBookId", "TagsTagId" },
                values: new object[,] {
                    { 1, "Bilim-Kurgu" },
                    { 2, "Korku" },

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Teest", "Test" });
        }
    }
}

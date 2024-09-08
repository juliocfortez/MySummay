using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOwnSummaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class changingModelNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Translate",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Translate",
                table: "Note");
        }
    }
}

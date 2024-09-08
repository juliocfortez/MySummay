using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOwnSummaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class addToModelNoteFieldPronunciation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Note",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Pronunciation",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceText",
                table: "Note",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pronunciation",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "SourceText",
                table: "Note");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Note",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Note",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}

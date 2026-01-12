using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout.Infrastructure.Database.Migration
{
    /// <inheritdoc />
    public partial class LinkUrlAdded : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "workout",
                table: "Workout",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                schema: "workout",
                table: "Exercise",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Active",
                columns: table => new
                {
                    Value = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Active");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "workout",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "Link",
                schema: "workout",
                table: "Exercise");
        }
    }
}

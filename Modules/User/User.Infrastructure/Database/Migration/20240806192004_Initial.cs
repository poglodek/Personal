using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Database.Migration
{
    /// <inheritdoc />
    public partial class Initial : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    MailAddress = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "jsonb", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastLogin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Blocked = table.Column<string>(type: "jsonb", nullable: true),
                    Activated = table.Column<string>(type: "jsonb", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "jsonb", nullable: false),
                    Claims = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");
        }
    }
}

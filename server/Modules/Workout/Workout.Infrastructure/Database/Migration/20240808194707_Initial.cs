using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout.Infrastructure.Database.Migration
{
    /// <inheritdoc />
    public partial class Initial : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "workout");

            migrationBuilder.CreateTable(
                name: "Date",
                columns: table => new
                {
                    Value = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlan",
                schema: "workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WardId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                schema: "workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: true),
                    _dates = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_WorkoutPlan_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "workout",
                        principalTable: "WorkoutPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                schema: "workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PrimaryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_Exercise_PrimaryId",
                        column: x => x.PrimaryId,
                        principalSchema: "workout",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "workout",
                        principalTable: "Workout",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Set",
                schema: "workout",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Repeat = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RestTime = table.Column<int>(type: "integer", nullable: false),
                    RepetitionRate = table.Column<string>(type: "jsonb", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    SetId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Set_Exercise_SetId",
                        column: x => x.SetId,
                        principalSchema: "workout",
                        principalTable: "Exercise",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Id_TrainerId",
                schema: "workout",
                table: "Exercise",
                columns: new[] { "Id", "TrainerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_PrimaryId",
                schema: "workout",
                table: "Exercise",
                column: "PrimaryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainerId",
                schema: "workout",
                table: "Exercise",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_WorkoutId",
                schema: "workout",
                table: "Exercise",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_SetId",
                schema: "workout",
                table: "Set",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_WorkoutId",
                schema: "workout",
                table: "Workout",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlan_Id_TrainerId",
                schema: "workout",
                table: "WorkoutPlan",
                columns: new[] { "Id", "TrainerId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlan_TrainerId",
                schema: "workout",
                table: "WorkoutPlan",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Date");

            migrationBuilder.DropTable(
                name: "Set",
                schema: "workout");

            migrationBuilder.DropTable(
                name: "Exercise",
                schema: "workout");

            migrationBuilder.DropTable(
                name: "Workout",
                schema: "workout");

            migrationBuilder.DropTable(
                name: "WorkoutPlan",
                schema: "workout");
        }
    }
}

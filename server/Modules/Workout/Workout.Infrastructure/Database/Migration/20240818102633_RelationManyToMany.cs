using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout.Infrastructure.Database.Migration
{
    /// <inheritdoc />
    public partial class RelationManyToMany : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Workout_WorkoutId",
                schema: "workout",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_WorkoutId",
                schema: "workout",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                schema: "workout",
                table: "Exercise");

            migrationBuilder.CreateTable(
                name: "ExerciseWorkout",
                schema: "workout",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkout", x => new { x.ExercisesId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Exercise_ExercisesId",
                        column: x => x.ExercisesId,
                        principalSchema: "workout",
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalSchema: "workout",
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkout_WorkoutId",
                schema: "workout",
                table: "ExerciseWorkout",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseWorkout",
                schema: "workout");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                schema: "workout",
                table: "Exercise",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_WorkoutId",
                schema: "workout",
                table: "Exercise",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Workout_WorkoutId",
                schema: "workout",
                table: "Exercise",
                column: "WorkoutId",
                principalSchema: "workout",
                principalTable: "Workout",
                principalColumn: "Id");
        }
    }
}

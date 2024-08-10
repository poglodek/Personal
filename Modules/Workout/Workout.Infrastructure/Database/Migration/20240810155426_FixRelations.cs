using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout.Infrastructure.Database.Migration
{
    /// <inheritdoc />
    public partial class FixRelations : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_Exercise_SetId",
                schema: "workout",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutPlan_WorkoutId",
                schema: "workout",
                table: "Workout");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                schema: "workout",
                table: "Workout",
                newName: "WorkoutPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_WorkoutId",
                schema: "workout",
                table: "Workout",
                newName: "IX_Workout_WorkoutPlanId");

            migrationBuilder.RenameColumn(
                name: "SetId",
                schema: "workout",
                table: "Set",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Set_SetId",
                schema: "workout",
                table: "Set",
                newName: "IX_Set_ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Exercise_ExerciseId",
                schema: "workout",
                table: "Set",
                column: "ExerciseId",
                principalSchema: "workout",
                principalTable: "Exercise",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutPlan_WorkoutPlanId",
                schema: "workout",
                table: "Workout",
                column: "WorkoutPlanId",
                principalSchema: "workout",
                principalTable: "WorkoutPlan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_Exercise_ExerciseId",
                schema: "workout",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_WorkoutPlan_WorkoutPlanId",
                schema: "workout",
                table: "Workout");

            migrationBuilder.RenameColumn(
                name: "WorkoutPlanId",
                schema: "workout",
                table: "Workout",
                newName: "WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_WorkoutPlanId",
                schema: "workout",
                table: "Workout",
                newName: "IX_Workout_WorkoutId");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                schema: "workout",
                table: "Set",
                newName: "SetId");

            migrationBuilder.RenameIndex(
                name: "IX_Set_ExerciseId",
                schema: "workout",
                table: "Set",
                newName: "IX_Set_SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Exercise_SetId",
                schema: "workout",
                table: "Set",
                column: "SetId",
                principalSchema: "workout",
                principalTable: "Exercise",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_WorkoutPlan_WorkoutId",
                schema: "workout",
                table: "Workout",
                column: "WorkoutId",
                principalSchema: "workout",
                principalTable: "WorkoutPlan",
                principalColumn: "Id");
        }
    }
}

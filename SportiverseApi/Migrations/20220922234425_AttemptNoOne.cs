using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class AttemptNoOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExerciseID",
                table: "PlanExercises",
                newName: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_ExerciseId",
                table: "PlanExercises",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseId",
                table: "PlanExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseId",
                table: "PlanExercises");

            migrationBuilder.DropIndex(
                name: "IX_PlanExercises_ExerciseId",
                table: "PlanExercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "PlanExercises",
                newName: "ExerciseID");
        }
    }
}

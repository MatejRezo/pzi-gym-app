using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class RemoveUnnecessaryProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseID",
                table: "PlanExercises");

            migrationBuilder.DropIndex(
                name: "IX_PlanExercises_ExerciseID",
                table: "PlanExercises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_ExerciseID",
                table: "PlanExercises",
                column: "ExerciseID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseID",
                table: "PlanExercises",
                column: "ExerciseID",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

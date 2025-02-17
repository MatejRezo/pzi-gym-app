using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class RevertTheRevert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_TrainingPlans_PlanID",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_PlanID",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "PlanID",
                table: "Exercises");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_PlanID",
                table: "PlanExercises",
                column: "PlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExercises_TrainingPlans_PlanID",
                table: "PlanExercises",
                column: "PlanID",
                principalTable: "TrainingPlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanExercises_TrainingPlans_PlanID",
                table: "PlanExercises");

            migrationBuilder.DropIndex(
                name: "IX_PlanExercises_PlanID",
                table: "PlanExercises");

            migrationBuilder.AddColumn<int>(
                name: "PlanID",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_PlanID",
                table: "Exercises",
                column: "PlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_TrainingPlans_PlanID",
                table: "Exercises",
                column: "PlanID",
                principalTable: "TrainingPlans",
                principalColumn: "ID");
        }
    }
}

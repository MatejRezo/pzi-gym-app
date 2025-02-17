using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class AddExercisePlansManyToMany : Migration
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

            migrationBuilder.CreateTable(
                name: "ExercisePlan",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "int", nullable: false),
                    PlansID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisePlan", x => new { x.ExercisesId, x.PlansID });
                    table.ForeignKey(
                        name: "FK_ExercisePlan_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExercisePlan_TrainingPlans_PlansID",
                        column: x => x.PlansID,
                        principalTable: "TrainingPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlan_PlansID",
                table: "ExercisePlan",
                column: "PlansID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisePlan");

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

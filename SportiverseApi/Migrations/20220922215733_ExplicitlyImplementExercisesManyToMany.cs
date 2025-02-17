using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class ExplicitlyImplementExercisesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisePlan");

            migrationBuilder.CreateTable(
                name: "PlanExercises",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    ExerciseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanExercises", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlanExercises_Exercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanExercises_TrainingPlans_PlanID",
                        column: x => x.PlanID,
                        principalTable: "TrainingPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_ExerciseID",
                table: "PlanExercises",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_PlanID",
                table: "PlanExercises",
                column: "PlanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanExercises");

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
    }
}

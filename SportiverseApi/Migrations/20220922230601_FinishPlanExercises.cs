using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class FinishPlanExercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanExercises");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PlanExercises",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseID = table.Column<int>(type: "int", nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanExercises", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlanExercises_TrainingPlans_PlanID",
                        column: x => x.PlanID,
                        principalTable: "TrainingPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_PlanID",
                table: "PlanExercises",
                column: "PlanID");
        }
    }
}

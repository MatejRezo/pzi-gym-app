using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class FinishPlanExercises2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanExercises");
        }
    }
}

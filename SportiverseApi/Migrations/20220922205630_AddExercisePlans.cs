using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class AddExercisePlans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanID",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.ID);
                });

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

            migrationBuilder.DropTable(
                name: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_PlanID",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "PlanID",
                table: "Exercises");
        }
    }
}

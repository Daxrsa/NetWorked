using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobService.Migrations
{
    /// <inheritdoc />
    public partial class JobCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "JobPositions");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_CategoryId",
                table: "JobPositions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPositions_Category_CategoryId",
                table: "JobPositions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPositions_Category_CategoryId",
                table: "JobPositions");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_JobPositions_CategoryId",
                table: "JobPositions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobPositions");

            migrationBuilder.AddColumn<string>(
                name: "JobCategory",
                table: "JobPositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

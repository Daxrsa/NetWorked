using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class example : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profesoret",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesoret", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studentet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studentet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfesoriStudenti",
                columns: table => new
                {
                    ProfesorsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoriStudenti", x => new { x.ProfesorsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ProfesoriStudenti_Profesoret_ProfesorsId",
                        column: x => x.ProfesorsId,
                        principalTable: "Profesoret",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesoriStudenti_Studentet_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Studentet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoriStudenti_StudentsId",
                table: "ProfesoriStudenti",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesoriStudenti");

            migrationBuilder.DropTable(
                name: "Profesoret");

            migrationBuilder.DropTable(
                name: "Studentet");
        }
    }
}

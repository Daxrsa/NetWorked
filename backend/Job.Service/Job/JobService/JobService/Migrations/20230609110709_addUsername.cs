using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobService.Migrations
{
    /// <inheritdoc />
    public partial class addUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "JobPositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "JobPositions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobService.Migrations
{
    /// <inheritdoc />
    public partial class updatedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicantName",
                table: "Applications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                columns: new[] { "JobId", "ApplicantId" });
        }
    }
}

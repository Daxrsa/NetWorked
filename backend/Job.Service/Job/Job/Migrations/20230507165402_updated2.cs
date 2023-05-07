using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job.Migrations
{
    /// <inheritdoc />
    public partial class updated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_JobPositions_JobPositonId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_JobPositonId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "JobPositonId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobPositions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_CategoryId",
                table: "JobPositions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPositions_Categories_CategoryId",
                table: "JobPositions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPositions_Categories_CategoryId",
                table: "JobPositions");

            migrationBuilder.DropIndex(
                name: "IX_JobPositions_CategoryId",
                table: "JobPositions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobPositions");

            migrationBuilder.AddColumn<Guid>(
                name: "JobPositonId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_JobPositonId",
                table: "Categories",
                column: "JobPositonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_JobPositions_JobPositonId",
                table: "Categories",
                column: "JobPositonId",
                principalTable: "JobPositions",
                principalColumn: "Id");
        }
    }
}

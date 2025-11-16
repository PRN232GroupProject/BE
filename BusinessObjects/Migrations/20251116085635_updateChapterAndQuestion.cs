using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class updateChapterAndQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Chapters_ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "ChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Chapters_ChapterId",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "ChapterId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddTestEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Users_UserId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "ChemistryPrepV1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AnsweredAt",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_UserId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_SessionId");

            migrationBuilder.CreateTable(
                name: "Tests",
                schema: "ChemistryPrepV1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "ChemistryPrepV1",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentTestSessions",
                schema: "ChemistryPrepV1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Score = table.Column<float>(type: "real", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTestSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTestSessions_Tests_TestId",
                        column: x => x.TestId,
                        principalSchema: "ChemistryPrepV1",
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTestSessions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ChemistryPrepV1",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                schema: "ChemistryPrepV1",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => new { x.TestId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_TestQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "ChemistryPrepV1",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestQuestions_Tests_TestId",
                        column: x => x.TestId,
                        principalSchema: "ChemistryPrepV1",
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestSessions_TestId",
                schema: "ChemistryPrepV1",
                table: "StudentTestSessions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestSessions_UserId",
                schema: "ChemistryPrepV1",
                table: "StudentTestSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_QuestionId",
                schema: "ChemistryPrepV1",
                table: "TestQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Tests",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Lessons",
                column: "CreatedById",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "LessonId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "CreatedById",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                column: "QuestionId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_StudentTestSessions_SessionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                column: "SessionId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "StudentTestSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "ChemistryPrepV1",
                table: "Users",
                column: "RoleId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_StudentTestSessions_SessionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "ChemistryPrepV1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "StudentTestSessions",
                schema: "ChemistryPrepV1");

            migrationBuilder.DropTable(
                name: "TestQuestions",
                schema: "ChemistryPrepV1");

            migrationBuilder.DropTable(
                name: "Tests",
                schema: "ChemistryPrepV1");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_SessionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AnsweredAt",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Lessons",
                column: "CreatedById",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "LessonId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_CreatedById",
                schema: "ChemistryPrepV1",
                table: "Questions",
                column: "CreatedById",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                column: "QuestionId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Users_UserId",
                schema: "ChemistryPrepV1",
                table: "StudentAnswers",
                column: "UserId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "ChemistryPrepV1",
                table: "Users",
                column: "RoleId",
                principalSchema: "ChemistryPrepV1",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

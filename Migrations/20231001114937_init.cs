using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraspItEz.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionStatuses",
                columns: table => new
                {
                    QueryStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QueryStatusValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionStatuses", x => x.QueryStatusId);
                });

            migrationBuilder.CreateTable(
                name: "StudySets",
                columns: table => new
                {
                    StudySetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUsed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySets", x => x.StudySetId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QueryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuestionStatus = table.Column<int>(type: "int", nullable: false),
                    AnswerStatus = table.Column<int>(type: "int", nullable: false),
                    QueryStatusId = table.Column<int>(type: "int", nullable: false),
                    StudySetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QueryId);
                    table.ForeignKey(
                        name: "FK_Questions_StudySets_StudySetId",
                        column: x => x.StudySetId,
                        principalTable: "StudySets",
                        principalColumn: "StudySetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_StudySetId",
                table: "Questions",
                column: "StudySetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionStatuses");

            migrationBuilder.DropTable(
                name: "StudySets");
        }
    }
}

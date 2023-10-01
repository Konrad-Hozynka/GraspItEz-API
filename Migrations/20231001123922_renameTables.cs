using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraspItEz.Migrations
{
    /// <inheritdoc />
    public partial class renameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_StudySets_StudySetId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionStatuses",
                table: "QuestionStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "QuestionStatuses",
                newName: "QueryStatuses");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Querist");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_StudySetId",
                table: "Querist",
                newName: "IX_Querist_StudySetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueryStatuses",
                table: "QueryStatuses",
                column: "QueryStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Querist",
                table: "Querist",
                column: "QueryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Querist_StudySets_StudySetId",
                table: "Querist",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "StudySetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Querist_StudySets_StudySetId",
                table: "Querist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueryStatuses",
                table: "QueryStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Querist",
                table: "Querist");

            migrationBuilder.RenameTable(
                name: "QueryStatuses",
                newName: "QuestionStatuses");

            migrationBuilder.RenameTable(
                name: "Querist",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_Querist_StudySetId",
                table: "Questions",
                newName: "IX_Questions_StudySetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionStatuses",
                table: "QuestionStatuses",
                column: "QueryStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "QueryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_StudySets_StudySetId",
                table: "Questions",
                column: "StudySetId",
                principalTable: "StudySets",
                principalColumn: "StudySetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

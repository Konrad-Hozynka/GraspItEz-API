using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraspItEz.Migrations
{
    /// <inheritdoc />
    public partial class changeQuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QestStatus",
                table: "Questions",
                newName: "QuestStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestStatus",
                table: "Questions",
                newName: "QestStatus");
        }
    }
}

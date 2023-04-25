using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraspItEz.Migrations
{
    /// <inheritdoc />
    public partial class progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Progres",
                table: "StudySets",
                newName: "Progress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Progress",
                table: "StudySets",
                newName: "Progres");
        }
    }
}

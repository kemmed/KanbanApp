using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanApp.Migrations
{
    /// <inheritdoc />
    public partial class Addarchive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignDate",
                table: "IssueColumn",
                newName: "DeleteDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IssueColumn",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IssueColumn");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "IssueColumn",
                newName: "AssignDate");
        }
    }
}

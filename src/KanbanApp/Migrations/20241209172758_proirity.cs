using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanApp.Migrations
{
    /// <inheritdoc />
    public partial class proirity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriorityTypeID",
                table: "Issue",
                newName: "Priority");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Issue",
                newName: "PriorityTypeID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanApp.Migrations
{
    /// <inheritdoc />
    public partial class proirityTypeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_PriorityType_PriorityTypeID",
                table: "Issue");

            migrationBuilder.DropTable(
                name: "PriorityType");

            migrationBuilder.DropIndex(
                name: "IX_Issue_PriorityTypeID",
                table: "Issue");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Issue",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Issue",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "PriorityType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriorityType_Board_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Board",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PriorityTypeID",
                table: "Issue",
                column: "PriorityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityType_BoardID",
                table: "PriorityType",
                column: "BoardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_PriorityType_PriorityTypeID",
                table: "Issue",
                column: "PriorityTypeID",
                principalTable: "PriorityType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanApp.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    HashPass = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatorUserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Board_User_CreatorUserID",
                        column: x => x.CreatorUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BoardID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Column_Board_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Board",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorityType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BoardID = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UserBoard",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    BoardID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserRole = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBoard_Board_BoardID",
                        column: x => x.BoardID,
                        principalTable: "Board",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBoard_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatorID = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformerID = table.Column<int>(type: "INTEGER", nullable: false),
                    PriorityTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Issue_PriorityType_PriorityTypeID",
                        column: x => x.PriorityTypeID,
                        principalTable: "PriorityType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_User_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_User_PerformerID",
                        column: x => x.PerformerID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueColumn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssignDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IssueID = table.Column<int>(type: "INTEGER", nullable: false),
                    ColumnID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueColumn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IssueColumn_Column_ColumnID",
                        column: x => x.ColumnID,
                        principalTable: "Column",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueColumn_Issue_IssueID",
                        column: x => x.IssueID,
                        principalTable: "Issue",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Board_CreatorUserID",
                table: "Board",
                column: "CreatorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Column_BoardID",
                table: "Column",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CreatorID",
                table: "Issue",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PerformerID",
                table: "Issue",
                column: "PerformerID");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PriorityTypeID",
                table: "Issue",
                column: "PriorityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueColumn_ColumnID",
                table: "IssueColumn",
                column: "ColumnID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueColumn_IssueID",
                table: "IssueColumn",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityType_BoardID",
                table: "PriorityType",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoard_BoardID",
                table: "UserBoard",
                column: "BoardID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBoard_UserID",
                table: "UserBoard",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueColumn");

            migrationBuilder.DropTable(
                name: "UserBoard");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "PriorityType");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

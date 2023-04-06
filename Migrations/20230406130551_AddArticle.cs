using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringboardHub_BE_101.Migrations
{
    /// <inheritdoc />
    public partial class AddArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleID = table.Column<int>(type: "int", nullable: false),
                    LectureID = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    visibaleType = table.Column<int>(type: "int", nullable: false),
                    isUpdated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Article_Lecture_LectureID",
                        column: x => x.LectureID,
                        principalTable: "Lecture",
                        principalColumn: "LectureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_Title_TitleID",
                        column: x => x.TitleID,
                        principalTable: "Title",
                        principalColumn: "TitleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_LectureID",
                table: "Article",
                column: "LectureID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TitleID",
                table: "Article",
                column: "TitleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}

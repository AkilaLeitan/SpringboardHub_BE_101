using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringboardHub_BE_101.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectSyllabusLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    LectureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.LectureID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    TextBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectInSyllabus",
                columns: table => new
                {
                    SubjectInSyllabusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    LectureID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectInSyllabus", x => x.SubjectInSyllabusID);
                    table.ForeignKey(
                        name: "FK_SubjectInSyllabus_Lecture_LectureID",
                        column: x => x.LectureID,
                        principalTable: "Lecture",
                        principalColumn: "LectureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectInSyllabus_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectInSyllabus_Syllabus_SyllabusID",
                        column: x => x.SyllabusID,
                        principalTable: "Syllabus",
                        principalColumn: "SyllabusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInSyllabus_LectureID",
                table: "SubjectInSyllabus",
                column: "LectureID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInSyllabus_SubjectID",
                table: "SubjectInSyllabus",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectInSyllabus_SyllabusID",
                table: "SubjectInSyllabus",
                column: "SyllabusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectInSyllabus");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}

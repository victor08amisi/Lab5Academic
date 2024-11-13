using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace LabAssignment4.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    HoursPerWeek = table.Column<int>(type: "int", nullable: true),
                    FeeBase = table.Column<decimal>(type: "decimal(6,2)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Code);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee_Role",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Employee_Id, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_ToEmployee",
                        column: x => x.Employee_Id,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ToRole",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AcademicRecord",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    StudentId = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.StudentId, x.CourseCode });
                    table.ForeignKey(
                        name: "FK_AcademicRecord_Course",
                        column: x => x.CourseCode,
                        principalTable: "Course",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_AcademicRecord_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Course_CourseID = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Student_StudentNum = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Course_CourseID, x.Student_StudentNum });
                    table.ForeignKey(
                        name: "FK_Registration_ToCourse",
                        column: x => x.Course_CourseID,
                        principalTable: "Course",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Registration_ToStudent",
                        column: x => x.Student_StudentNum,
                        principalTable: "Student",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "FK_AcademicRecord_Course",
                table: "AcademicRecord",
                column: "CourseCode");

            migrationBuilder.CreateIndex(
                name: "FK_ToRole",
                table: "Employee_Role",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "FK_Registration_ToStudent",
                table: "Registration",
                column: "Student_StudentNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicRecord");

            migrationBuilder.DropTable(
                name: "Employee_Role");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}

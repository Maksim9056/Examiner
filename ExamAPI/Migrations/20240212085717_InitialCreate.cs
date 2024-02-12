using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExamAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerOptions = table.Column<string>(type: "text", nullable: true),
                    CorrectAnswers = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_exam = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionName = table.Column<string>(type: "text", nullable: true),
                    AnswerTrue = table.Column<string>(type: "text", nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_roles = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Test = table.Column<string>(type: "text", nullable: true),
                    Options_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Employee = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    DataMess = table.Column<string>(type: "text", nullable: true),
                    EmailId = table.Column<int>(type: "integer", nullable: true),
                    Id_roles_users = table.Column<int>(type: "integer", nullable: false),
                    Employee_Mail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Filles_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Filles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionsId = table.Column<int>(type: "integer", nullable: true),
                    CorrectAnswers = table.Column<string>(type: "text", nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    AnswerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExamsTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExamsId = table.Column<int>(type: "integer", nullable: true),
                    TestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamsTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamsTest_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamsTest_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_AnswerId = table.Column<int>(type: "integer", nullable: true),
                    Test_Name = table.Column<string>(type: "text", nullable: true),
                    QuestionsId = table.Column<int>(type: "integer", nullable: true),
                    Questions_IdId = table.Column<int>(type: "integer", nullable: true),
                    Id_TestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Answer_Id_AnswerId",
                        column: x => x.Id_AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Questions_Questions_IdId",
                        column: x => x.Questions_IdId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Test_Id_TestId",
                        column: x => x.Id_TestId,
                        principalTable: "Test",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTestId = table.Column<int>(type: "integer", nullable: true),
                    IdQuestionsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestion_Questions_IdQuestionsId",
                        column: x => x.IdQuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestQuestion_Test_IdTestId",
                        column: x => x.IdTestId,
                        principalTable: "Test",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_Exam = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    QuesttionId = table.Column<int>(type: "integer", nullable: true),
                    ExamsId = table.Column<int>(type: "integer", nullable: true),
                    Data_of_Exam = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Grade_Exam = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exam_Questions_QuesttionId",
                        column: x => x.QuesttionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exam_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Save_results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionsId = table.Column<int>(type: "integer", nullable: true),
                    Name_TestId = table.Column<int>(type: "integer", nullable: true),
                    Users_Answers_Questions = table.Column<string>(type: "text", nullable: true),
                    Exam_idId = table.Column<int>(type: "integer", nullable: true),
                    Date_of_Result_Exam_Endings = table.Column<string>(type: "text", nullable: true),
                    Name_Users = table.Column<string>(type: "text", nullable: true),
                    Resukts_exam = table.Column<int>(type: "integer", nullable: false),
                    User_idId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Save_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Save_results_Exams_Exam_idId",
                        column: x => x.Exam_idId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Save_results_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Save_results_Test_Name_TestId",
                        column: x => x.Name_TestId,
                        principalTable: "Test",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Save_results_User_User_idId",
                        column: x => x.User_idId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ExamsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExams_Exams_ExamsId",
                        column: x => x.ExamsId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserExams_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_rolesId = table.Column<int>(type: "integer", nullable: true),
                    User_idId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_roles_Roles_Id_rolesId",
                        column: x => x.Id_rolesId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_roles_User_User_idId",
                        column: x => x.User_idId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ExamsId",
                table: "Exam",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_QuesttionId",
                table: "Exam",
                column: "QuesttionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_UserId",
                table: "Exam",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsTest_ExamsId",
                table: "ExamsTest",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsTest_TestId",
                table: "ExamsTest",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Id_AnswerId",
                table: "Options",
                column: "Id_AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Id_TestId",
                table: "Options",
                column: "Id_TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionsId",
                table: "Options",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Questions_IdId",
                table: "Options",
                column: "Questions_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_AnswerId",
                table: "QuestionAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_QuestionsId",
                table: "QuestionAnswer",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_results_Exam_idId",
                table: "Save_results",
                column: "Exam_idId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_results_Name_TestId",
                table: "Save_results",
                column: "Name_TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_results_QuestionsId",
                table: "Save_results",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Save_results_User_idId",
                table: "Save_results",
                column: "User_idId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_IdQuestionsId",
                table: "TestQuestion",
                column: "IdQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_IdTestId",
                table: "TestQuestion",
                column: "IdTestId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailId",
                table: "User",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExams_ExamsId",
                table: "UserExams",
                column: "ExamsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExams_UserId",
                table: "UserExams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roles_Id_rolesId",
                table: "User_roles",
                column: "Id_rolesId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roles_User_idId",
                table: "User_roles",
                column: "User_idId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "ExamsTest");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "Save_results");

            migrationBuilder.DropTable(
                name: "TestQuestion");

            migrationBuilder.DropTable(
                name: "UserExams");

            migrationBuilder.DropTable(
                name: "User_roles");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Filles");
        }
    }
}

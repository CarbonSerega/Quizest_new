using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizest.Migrations
{
    public partial class CreateDb_And_Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AmountOfPasses = table.Column<int>(type: "int", nullable: true),
                    AmountOfLikes = table.Column<int>(type: "int", nullable: true),
                    AmountOfQuestions = table.Column<int>(type: "int", nullable: false),
                    Complexity = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizInfo_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfCorrectQuestions = table.Column<int>(type: "int", nullable: false),
                    SpentTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Mark = table.Column<float>(type: "real", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuizInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerInfo_QuizInfo_QuizInfoId",
                        column: x => x.QuizInfoId,
                        principalTable: "QuizInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizInfoUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizInfoUser", x => new { x.QuizInfoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_QuizInfoUser_QuizInfo_QuizInfoId",
                        column: x => x.QuizInfoId,
                        principalTable: "QuizInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizInfoUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarPath", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { new Guid("004efcbd-4197-4975-9e9e-1feb02c8d429"), "", "initial_email@example.com", "InititalFirstName", "InititalLastName", 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarPath", "Email", "FirstName", "LastName", "Role" },
                values: new object[] { new Guid("004efcbd-4197-4975-9e9e-1feb02c8d428"), "", "owner_email@example.com", "OwnerFirstName", "OwnerLastName", 0 });

            migrationBuilder.InsertData(
                table: "QuizInfo",
                columns: new[] { "Id", "AmountOfLikes", "AmountOfPasses", "AmountOfQuestions", "ClosedAt", "Complexity", "CreatedAt", "Description", "Duration", "IsLiked", "Name", "OwnerId", "UpdatedAt" },
                values: new object[] { new Guid("004efcbd-4197-4975-9e9e-1feb02c8d430"), 0, 0, 1, new DateTime(2020, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2020, 11, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "It is just an initial quiz for seeding", new TimeSpan(0, 0, 1, 20, 0), false, "Initial Quiz", new Guid("004efcbd-4197-4975-9e9e-1feb02c8d428"), new DateTime(2020, 11, 20, 1, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AnswerInfo",
                columns: new[] { "Id", "AmountOfCorrectQuestions", "Mark", "QuizInfoId", "SpentTime", "UserId" },
                values: new object[] { new Guid("004efcbd-4197-4975-9e9e-1feb02c8d432"), 1, 5f, new Guid("004efcbd-4197-4975-9e9e-1feb02c8d430"), new TimeSpan(0, 1, 10, 0, 0), new Guid("004efcbd-4197-4975-9e9e-1feb02c8d429") });

            migrationBuilder.InsertData(
                table: "QuizInfoUser",
                columns: new[] { "QuizInfoId", "UserId", "Id" },
                values: new object[] { new Guid("004efcbd-4197-4975-9e9e-1feb02c8d430"), new Guid("004efcbd-4197-4975-9e9e-1feb02c8d429"), new Guid("004efcbd-4197-4975-9e9e-1feb02c8d431") });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerInfo_QuizInfoId",
                table: "AnswerInfo",
                column: "QuizInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerInfo_UserId",
                table: "AnswerInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizInfo_OwnerId",
                table: "QuizInfo",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizInfoUser_UserId",
                table: "QuizInfoUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerInfo");

            migrationBuilder.DropTable(
                name: "QuizInfoUser");

            migrationBuilder.DropTable(
                name: "QuizInfo");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

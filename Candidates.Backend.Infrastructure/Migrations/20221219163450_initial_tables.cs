using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidates.Backend.Infrastructure.Migrations
{
    public partial class initial_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(150)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "CandidateExperiences",
                columns: table => new
                {
                    CandidateExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "varchar(200)", nullable: false),
                    Job = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(400)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExperiences", x => x.CandidateExperienceId);
                    table.ForeignKey(
                        name: "FK_CandidateExperiences_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExperiences_CandidateExperienceId_CandidateId",
                table: "CandidateExperiences",
                columns: new[] { "CandidateExperienceId", "CandidateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateExperiences_CandidateId",
                table: "CandidateExperiences",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CandidateId",
                table: "Candidates",
                column: "CandidateId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateExperiences");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}

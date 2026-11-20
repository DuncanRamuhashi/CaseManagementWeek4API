using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseManagementWeek4API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CourtCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CourtLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courts__3214EC075632CF21", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Defendants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "South African"),
                    IsInCustody = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Defendan__3214EC07C01ABE77", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OriginalCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "Normal"),
                    ReportedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Original__3214EC078414C7EF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prosecutors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Office = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prosecut__3214EC07F3B4291C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CourtId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Judges__3214EC07873C6808", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Judges_Courts",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourtCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    OriginalCaseId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: true),
                    JudgeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProsecutorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CaseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FilingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NextHearingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourtCas__3214EC0719BF172D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtCases_Courts",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourtCases_Judges",
                        column: x => x.JudgeId,
                        principalTable: "Judges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourtCases_OriginalCases",
                        column: x => x.OriginalCaseId,
                        principalTable: "OriginalCases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourtCases_Prosecutors",
                        column: x => x.ProsecutorId,
                        principalTable: "Prosecutors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourtCaseId = table.Column<int>(type: "int", nullable: false),
                    ChargeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Statute = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Plea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Charges__3214EC07FC40ED48", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_CourtCases",
                        column: x => x.CourtCaseId,
                        principalTable: "CourtCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourtCaseDefendants",
                columns: table => new
                {
                    CourtCaseId = table.Column<int>(type: "int", nullable: false),
                    DefendantId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "Accused"),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtCaseDefendants", x => new { x.CourtCaseId, x.DefendantId });
                    table.ForeignKey(
                        name: "FK_CCD_CourtCases",
                        column: x => x.CourtCaseId,
                        principalTable: "CourtCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CCD_Defendants",
                        column: x => x.DefendantId,
                        principalTable: "Defendants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_CourtCaseId",
                table: "Charges",
                column: "CourtCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCaseDefendants_DefendantId",
                table: "CourtCaseDefendants",
                column: "DefendantId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_CourtId",
                table: "CourtCases",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_JudgeId",
                table: "CourtCases",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_NextHearingDate",
                table: "CourtCases",
                column: "NextHearingDate");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_OriginalCaseId",
                table: "CourtCases",
                column: "OriginalCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_ProsecutorId",
                table: "CourtCases",
                column: "ProsecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtCases_Status",
                table: "CourtCases",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Defendants_IDNumber",
                table: "Defendants",
                column: "IDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Judges_CourtId",
                table: "Judges",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_OriginalCases_CaseNumber",
                table: "OriginalCases",
                column: "CaseNumber");

            migrationBuilder.CreateIndex(
                name: "UQ_OriginalCases_CaseNumber",
                table: "OriginalCases",
                column: "CaseNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "CourtCaseDefendants");

            migrationBuilder.DropTable(
                name: "CourtCases");

            migrationBuilder.DropTable(
                name: "Defendants");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "OriginalCases");

            migrationBuilder.DropTable(
                name: "Prosecutors");

            migrationBuilder.DropTable(
                name: "Courts");
        }
    }
}

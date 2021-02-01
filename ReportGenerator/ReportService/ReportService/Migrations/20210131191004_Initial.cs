using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logging");

            migrationBuilder.EnsureSchema(
                name: "reporting");

            migrationBuilder.CreateTable(
                name: "F_SERVICE_LOGS",
                schema: "logging",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceMethod = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_SERVICE_LOGS", x => new { x.LogMessage, x.LogTime, x.ServiceId, x.ServiceMethod });
                });

            migrationBuilder.CreateTable(
                name: "V_Report_Overview",
                schema: "reporting",
                columns: table => new
                {
                    Report_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Report_Sql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Report_Separator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Header_Row = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Report_Overview", x => x.Report_Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "F_SERVICE_LOGS",
                schema: "logging");

            migrationBuilder.DropTable(
                name: "V_Report_Overview",
                schema: "reporting");
        }
    }
}

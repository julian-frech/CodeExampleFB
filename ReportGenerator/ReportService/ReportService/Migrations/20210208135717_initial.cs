using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logging");

            migrationBuilder.EnsureSchema(
                name: "reporting");

            migrationBuilder.CreateTable(
                name: "LOGS",
                schema: "logging",
                columns: table => new
                {
                    Log_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Service_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logging_Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Log_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thread_Id = table.Column<int>(type: "int", nullable: false),
                    Machine_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Service_Step_Num = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solved = table.Column<int>(type: "int", nullable: false),
                    ITS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UTS = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGS", x => x.Log_Id);
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
                name: "LOGS",
                schema: "logging");

            migrationBuilder.DropTable(
                name: "V_Report_Overview",
                schema: "reporting");
        }
    }
}

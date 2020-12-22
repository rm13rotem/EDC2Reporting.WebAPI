using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDC2Reporting.WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    HelsinkiApprovalNumber = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperimentId = table.Column<int>(nullable: false),
                    VisitGroupId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    DataInJson = table.Column<string>(nullable: false),
                    CurrentDataInJson = table.Column<string>(nullable: true),
                    LastUpdator = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(nullable: false),
                    CurrentLastUpdateDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistantEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreateDate = table.Column<string>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistantEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "ModuleInfos");
        }
    }
}

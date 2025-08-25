using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataServices.Migrations
{
    public partial class Crf_25Aug2025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(maxLength: 40, nullable: false),
                    Action = table.Column<string>(maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(maxLength: 4000, nullable: false),
                    EntityId = table.Column<string>(nullable: false),
                    TimestampUtc = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ChangesJson = table.Column<string>(nullable: true),
                    BeforeJson = table.Column<string>(maxLength: 4000, nullable: true),
                    AfterJson = table.Column<string>(maxLength: 4000, nullable: true),
                    MetadataJson = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrfPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Html = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsLockedForChanges = table.Column<bool>(nullable: false),
                    GuidId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrfPages", x => x.Id);
                });

           
            //migrationBuilder.CreateTable(
            //    name: "ModuleInfos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ExperimentId = table.Column<int>(nullable: false),
            //        VisitGroupId = table.Column<int>(nullable: false),
            //        VisitId = table.Column<int>(nullable: false),
            //        DoctorId = table.Column<int>(nullable: false),
            //        PatientId = table.Column<int>(nullable: false),
            //        ModuleId = table.Column<int>(nullable: false),
            //        DataInJson = table.Column<string>(nullable: false),
            //        CurrentDataInJson = table.Column<string>(nullable: true),
            //        LastUpdator = table.Column<int>(nullable: false),
            //        CreatedDateTime = table.Column<DateTime>(nullable: false),
            //        LastUpdatedDateTime = table.Column<DateTime>(nullable: false),
            //        CurrentLastUpdateDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ModuleInfos", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersistentEntity",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        GuidId = table.Column<string>(maxLength: 100, nullable: false),
            //        EntityName = table.Column<string>(maxLength: 100, nullable: false),
            //        CreateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        Name = table.Column<string>(maxLength: 100, nullable: false),
            //        JsonValue = table.Column<string>(nullable: false),
            //        IsDeleted = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersistentEntity", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "CrfEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false),
                    VisitIndex = table.Column<int>(nullable: false),
                    StudyId = table.Column<int>(nullable: false),
                    CrfPageId = table.Column<int>(nullable: false),
                    FormDataJson = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrfEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrfEntries_CrfPages_CrfPageId",
                        column: x => x.CrfPageId,
                        principalTable: "CrfPages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrfEntries_CrfPageId",
                table: "CrfEntries",
                column: "CrfPageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CrfEntries");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "ModuleInfos");

            migrationBuilder.DropTable(
                name: "PersistentEntity");

            migrationBuilder.DropTable(
                name: "CrfPages");
        }
    }
}

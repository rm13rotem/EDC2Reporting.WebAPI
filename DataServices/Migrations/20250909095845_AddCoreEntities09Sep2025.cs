using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServices.Migrations
{
    /// <inheritdoc />
    public partial class AddCoreEntities09Sep2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleInfos",
                table: "ModuleInfos");

            migrationBuilder.RenameTable(
                name: "ModuleInfos",
                newName: "ModuleInfo");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PersistentEntity",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudyId",
                table: "PersistentEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectIdInTrial",
                table: "PersistentEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectInitials",
                table: "PersistentEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "CrfPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleInfo",
                table: "ModuleInfo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "VisitGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitGroups_VisitGroups_VisitGroupId",
                        column: x => x.VisitGroupId,
                        principalTable: "VisitGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalIndex = table.Column<int>(type: "int", nullable: false),
                    GuidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_VisitGroups_VisitGroupId",
                        column: x => x.VisitGroupId,
                        principalTable: "VisitGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrfPages_VisitId",
                table: "CrfPages",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitGroups_VisitGroupId",
                table: "VisitGroups",
                column: "VisitGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitGroupId",
                table: "Visits",
                column: "VisitGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrfPages_Visits_VisitId",
                table: "CrfPages",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrfPages_Visits_VisitId",
                table: "CrfPages");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "VisitGroups");

            migrationBuilder.DropIndex(
                name: "IX_CrfPages_VisitId",
                table: "CrfPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleInfo",
                table: "ModuleInfo");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "StudyId",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "SubjectIdInTrial",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "SubjectInitials",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "CrfPages");

            migrationBuilder.RenameTable(
                name: "ModuleInfo",
                newName: "ModuleInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleInfos",
                table: "ModuleInfos",
                column: "Id");
        }
    }
}

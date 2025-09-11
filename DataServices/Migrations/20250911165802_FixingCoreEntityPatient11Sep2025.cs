using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServices.Migrations
{
    /// <inheritdoc />
    public partial class FixingCoreEntityPatient11Sep2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyId",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "SubjectIdInTrial",
                table: "PersistentEntity");

            migrationBuilder.DropColumn(
                name: "SubjectInitials",
                table: "PersistentEntity");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectIdInTrial = table.Column<int>(type: "int", nullable: false),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    SubjectInitials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

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
        }
    }
}

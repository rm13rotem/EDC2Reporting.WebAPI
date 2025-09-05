using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServices.Migrations
{
    /// <inheritdoc />
    public partial class FineTuningTheCrfEntry05Sep2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "CrfEntries",
                newName: "InvestigatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CrfEntries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "GuidId",
                table: "CrfEntries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "CrfEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdator",
                table: "CrfEntries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "CrfEntries");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "CrfEntries");

            migrationBuilder.DropColumn(
                name: "LastUpdator",
                table: "CrfEntries");

            migrationBuilder.RenameColumn(
                name: "InvestigatorId",
                table: "CrfEntries",
                newName: "DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CrfEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataServices.Migrations
{
    /// <inheritdoc />
    public partial class AuditTrailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.AuditLogs', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AuditLogs] (
        [Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserId] NVARCHAR(40) NOT NULL,
        [Action] NVARCHAR(100) NOT NULL,
        [EntityName] NVARCHAR(4000) NOT NULL,
        [EntityId] NVARCHAR(4000) NOT NULL,
        [TimestampUtc] DATETIME2 NOT NULL DEFAULT (GETDATE()),
        [ChangesJson] NVARCHAR(4000) NULL,
        [BeforeJson] NVARCHAR(4000) NULL,
        [AfterJson] NVARCHAR(4000) NULL,
        [MetadataJson] NVARCHAR(500) NULL
    );
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.AuditLogs', N'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[AuditLogs];
END
");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharmaManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterTable",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTable", x => x.PharmacyId);
                });

            migrationBuilder.InsertData(
                table: "MasterTable",
                columns: new[] { "PharmacyId", "DbName", "Email", "IsActive", "PasswordHash", "PharmacyName" },
                values: new object[,]
                {
                    { 1, "ChildPharma1", "pharma1@mail.com", true, "$2a$11$SNU6ITIfH5raJut0TwCkUeQdEqk9BDiVmABkj5GP3G.JL7V.skbsW", "Pharma-1" },
                    { 2, "ChildPharma2", "pharma2@mail.com", true, "$2a$11$T6Ovkdcntdx.lErzlZDn4ekJxUBqIWB0B/oIeGvNvdLAre.p2KY1y", "Pharma-2" },
                    { 3, "ChildPharma3", "pharma3@mail.com", true, "$2a$11$iuagNdx9.uQe7VizVbdG4.y5NmaVuMGFdXA1pcIhrhrZoaDsrnCVW", "Pharma-3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterTable");
        }
    }
}

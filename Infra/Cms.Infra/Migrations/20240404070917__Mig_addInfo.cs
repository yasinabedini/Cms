using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infra.Migrations
{
    /// <inheritdoc />
    public partial class _Mig_addInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Info",
                columns: new[] { "Id", "Address", "CreateAt", "EmailAddress", "InstagramAddress", "IsDelete", "IsEnable", "ModifiedAt", "PhoneNumber", "WorkTime" },
                values: new object[] { 1L, "", new DateTime(2024, 4, 4, 10, 39, 16, 235, DateTimeKind.Local).AddTicks(8468), "aaaa@domain.com", "", false, true, null, "00-00000000", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Info");
        }
    }
}

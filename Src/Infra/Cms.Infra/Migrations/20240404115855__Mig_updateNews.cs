using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infra.Migrations
{
    /// <inheritdoc />
    public partial class _Mig_updateNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Info",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "SeconodParagraph",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ThirdParagraph",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "FirstParagraph",
                table: "News",
                newName: "Text");

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "Info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Info_LanguageId",
                table: "Info",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Info_Languages_LanguageId",
                table: "Info",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Info_Languages_LanguageId",
                table: "Info");

            migrationBuilder.DropIndex(
                name: "IX_Info_LanguageId",
                table: "Info");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Info");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "News",
                newName: "FirstParagraph");

            migrationBuilder.AddColumn<string>(
                name: "SeconodParagraph",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdParagraph",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Info",
                columns: new[] { "Id", "Address", "CreateAt", "EmailAddress", "InstagramAddress", "IsDelete", "IsEnable", "ModifiedAt", "PhoneNumber", "WorkTime" },
                values: new object[] { 1L, "", new DateTime(2024, 4, 4, 10, 39, 16, 235, DateTimeKind.Local).AddTicks(8468), "aaaa@domain.com", "", false, true, null, "00-00000000", "" });
        }
    }
}

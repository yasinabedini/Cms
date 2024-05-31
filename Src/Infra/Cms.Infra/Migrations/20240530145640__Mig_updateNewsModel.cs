using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infra.Migrations
{
    /// <inheritdoc />
    public partial class _Mig_updateNewsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbNailImage",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbNailImage",
                table: "News");
        }
    }
}

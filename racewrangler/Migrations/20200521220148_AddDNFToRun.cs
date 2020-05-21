using Microsoft.EntityFrameworkCore.Migrations;

namespace racewrangler.Migrations
{
    public partial class AddDNFToRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DNF",
                table: "Runs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNF",
                table: "Runs");
        }
    }
}

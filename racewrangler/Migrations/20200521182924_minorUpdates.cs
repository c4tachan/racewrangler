using Microsoft.EntityFrameworkCore.Migrations;

namespace racewrangler.Migrations
{
    public partial class minorUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberNumber",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberNumber",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");
        }
    }
}

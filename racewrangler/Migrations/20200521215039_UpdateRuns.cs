using Microsoft.EntityFrameworkCore.Migrations;

namespace racewrangler.Migrations
{
    public partial class UpdateRuns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Run_Competitions_CompID",
                table: "Run");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_RaceEntries_RaceEntryID",
                table: "Run");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Run",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Run_CompID",
                table: "Run");

            migrationBuilder.DropColumn(
                name: "CompID",
                table: "Run");

            migrationBuilder.RenameTable(
                name: "Run",
                newName: "Runs");

            migrationBuilder.RenameIndex(
                name: "IX_Run_RaceEntryID",
                table: "Runs",
                newName: "IX_Runs_RaceEntryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Runs",
                table: "Runs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_RaceEntries_RaceEntryID",
                table: "Runs",
                column: "RaceEntryID",
                principalTable: "RaceEntries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_RaceEntries_RaceEntryID",
                table: "Runs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Runs",
                table: "Runs");

            migrationBuilder.RenameTable(
                name: "Runs",
                newName: "Run");

            migrationBuilder.RenameIndex(
                name: "IX_Runs_RaceEntryID",
                table: "Run",
                newName: "IX_Run_RaceEntryID");

            migrationBuilder.AddColumn<int>(
                name: "CompID",
                table: "Run",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Run",
                table: "Run",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_CompID",
                table: "Run",
                column: "CompID");

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Competitions_CompID",
                table: "Run",
                column: "CompID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_RaceEntries_RaceEntryID",
                table: "Run",
                column: "RaceEntryID",
                principalTable: "RaceEntries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

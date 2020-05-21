using Microsoft.EntityFrameworkCore.Migrations;

namespace racewrangler.Migrations
{
    public partial class UpdateRaceEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Competitions_CompetitionID",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntry_Competitions_CompetitionID",
                table: "RaceEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntry_Drivers_DriverID",
                table: "RaceEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_RaceEntry_RaceEntryID",
                table: "Run");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_CompetitionID",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DriverID",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceEntry",
                table: "RaceEntry");

            migrationBuilder.DropColumn(
                name: "CompetitionID",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "RaceEntry",
                newName: "RaceEntries");

            migrationBuilder.RenameIndex(
                name: "IX_RaceEntry_DriverID",
                table: "RaceEntries",
                newName: "IX_RaceEntries_DriverID");

            migrationBuilder.RenameIndex(
                name: "IX_RaceEntry_CompetitionID",
                table: "RaceEntries",
                newName: "IX_RaceEntries_CompetitionID");

            migrationBuilder.AddColumn<int>(
                name: "Penalties",
                table: "Run",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Drivers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Drivers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarID",
                table: "RaceEntries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassID",
                table: "RaceEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "RaceEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sponsor",
                table: "RaceEntries",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceEntries",
                table: "RaceEntries",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEntries_CarID",
                table: "RaceEntries",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEntries_ClassID",
                table: "RaceEntries",
                column: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntries_Cars_CarID",
                table: "RaceEntries",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntries_Classes_ClassID",
                table: "RaceEntries",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntries_Competitions_CompetitionID",
                table: "RaceEntries",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntries_Drivers_DriverID",
                table: "RaceEntries",
                column: "DriverID",
                principalTable: "Drivers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntries_Cars_CarID",
                table: "RaceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntries_Classes_ClassID",
                table: "RaceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntries_Competitions_CompetitionID",
                table: "RaceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceEntries_Drivers_DriverID",
                table: "RaceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_RaceEntries_RaceEntryID",
                table: "Run");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceEntries",
                table: "RaceEntries");

            migrationBuilder.DropIndex(
                name: "IX_RaceEntries_CarID",
                table: "RaceEntries");

            migrationBuilder.DropIndex(
                name: "IX_RaceEntries_ClassID",
                table: "RaceEntries");

            migrationBuilder.DropColumn(
                name: "Penalties",
                table: "Run");

            migrationBuilder.DropColumn(
                name: "CarID",
                table: "RaceEntries");

            migrationBuilder.DropColumn(
                name: "ClassID",
                table: "RaceEntries");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "RaceEntries");

            migrationBuilder.DropColumn(
                name: "Sponsor",
                table: "RaceEntries");

            migrationBuilder.RenameTable(
                name: "RaceEntries",
                newName: "RaceEntry");

            migrationBuilder.RenameIndex(
                name: "IX_RaceEntries_DriverID",
                table: "RaceEntry",
                newName: "IX_RaceEntry_DriverID");

            migrationBuilder.RenameIndex(
                name: "IX_RaceEntries_CompetitionID",
                table: "RaceEntry",
                newName: "IX_RaceEntry_CompetitionID");

            migrationBuilder.AlterColumn<int>(
                name: "LastName",
                table: "Drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstName",
                table: "Drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetitionID",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceEntry",
                table: "RaceEntry",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompetitionID",
                table: "Drivers",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverID",
                table: "Cars",
                column: "DriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Competitions_CompetitionID",
                table: "Drivers",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntry_Competitions_CompetitionID",
                table: "RaceEntry",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceEntry_Drivers_DriverID",
                table: "RaceEntry",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_RaceEntry_RaceEntryID",
                table: "Run",
                column: "RaceEntryID",
                principalTable: "RaceEntry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace racewrangler.Migrations
{
    public partial class AddOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    SeasonID = table.Column<int>(nullable: true),
                    EventNum = table.Column<int>(nullable: false),
                    EventSiteID = table.Column<int>(nullable: true),
                    OrgID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Competitions_Sites_EventSiteID",
                        column: x => x.EventSiteID,
                        principalTable: "Sites",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitions_Organizers_OrgID",
                        column: x => x.OrgID,
                        principalTable: "Organizers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitions_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<int>(nullable: false),
                    LastName = table.Column<int>(nullable: false),
                    CompetitionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Drivers_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DriverID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverID = table.Column<int>(nullable: true),
                    CompID = table.Column<int>(nullable: true),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Points_Competitions_CompID",
                        column: x => x.CompID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Points_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceEntry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverID = table.Column<int>(nullable: true),
                    CompetitionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RaceEntry_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceEntry_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Run",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompID = table.Column<int>(nullable: true),
                    RaceEntryID = table.Column<int>(nullable: true),
                    RunNumber = table.Column<int>(nullable: false),
                    LapTime = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Run", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Run_Competitions_CompID",
                        column: x => x.CompID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Run_RaceEntry_RaceEntryID",
                        column: x => x.RaceEntryID,
                        principalTable: "RaceEntry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverID",
                table: "Cars",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_EventSiteID",
                table: "Competitions",
                column: "EventSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_OrgID",
                table: "Competitions",
                column: "OrgID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonID",
                table: "Competitions",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CompetitionID",
                table: "Drivers",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_CompID",
                table: "Points",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Points_DriverID",
                table: "Points",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEntry_CompetitionID",
                table: "RaceEntry",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEntry_DriverID",
                table: "RaceEntry",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_CompID",
                table: "Run",
                column: "CompID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_RaceEntryID",
                table: "Run",
                column: "RaceEntryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Run");

            migrationBuilder.DropTable(
                name: "RaceEntry");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Organizers");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}

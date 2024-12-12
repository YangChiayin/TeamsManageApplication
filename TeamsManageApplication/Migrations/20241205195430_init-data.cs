using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamsManageApplication.Migrations
{
    /// <inheritdoc />
    public partial class initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpposingTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "DateCreated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toronto Maple Leafs" },
                    { 2, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Winnipeg Jets" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Date", "GameType", "OpposingTeamName", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", "Edmonton Oilers", 1 },
                    { 2, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", "Calgary Flames", 1 },
                    { 3, new DateTime(2023, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Away", "Montreal Canadians", 2 },
                    { 4, new DateTime(2023, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Away", "Ottawa Senators", 2 },
                    { 5, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", "New York Rangers", 2 },
                    { 6, new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", "Chicago Blackhawks", 1 },
                    { 7, new DateTime(2023, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Away", "Detroit Redwings", 1 },
                    { 8, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Away", "Boston Bruins", 2 },
                    { 9, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home", "Philadelphia Flyers", 2 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "FirstName", "LastName", "LeagueRegistrationNumber", "Number", "TeamId" },
                values: new object[,]
                {
                    { 1, "Bart", "Simpson", "DF-17345677", 9, 1 },
                    { 2, "Lisa", "Simpson", "AB-99685633", 11, 1 },
                    { 3, "Maggie", "Simpson", "FG-87455945", 18, 2 },
                    { 4, "Marge", "Simpson", "LM-11409213", 27, 2 },
                    { 5, "Homer", "Simpson", "ZC-59308990", 31, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamId",
                table: "Games",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}

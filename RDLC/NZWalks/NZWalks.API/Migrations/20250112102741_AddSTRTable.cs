using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSTRTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STRs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OneB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Three = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Four = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Five = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SixA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SixB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SixC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STRs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STRs");
        }
    }
}

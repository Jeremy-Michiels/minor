using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minor.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aPIKeys",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    signUpDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NextBillingDue = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aPIKeys", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aPIKeys");
        }
    }
}

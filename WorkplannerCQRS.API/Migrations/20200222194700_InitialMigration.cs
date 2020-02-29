using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplannerCQRS.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    ObjectNumber = table.Column<string>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: true),
                    EndDate = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.ObjectNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}

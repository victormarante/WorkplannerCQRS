using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkplannerCQRS.API.Migrations
{
    public partial class AddWorkerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrders",
                table: "WorkOrders");

            migrationBuilder.RenameTable(
                name: "WorkOrders",
                newName: "WorkOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrder",
                table: "WorkOrder",
                column: "ObjectNumber");

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrder",
                table: "WorkOrder");

            migrationBuilder.RenameTable(
                name: "WorkOrder",
                newName: "WorkOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrders",
                table: "WorkOrders",
                column: "ObjectNumber");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkMangement.Migrations
{
    public partial class AddEmployeeIdForWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Works",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Works");
        }
    }
}

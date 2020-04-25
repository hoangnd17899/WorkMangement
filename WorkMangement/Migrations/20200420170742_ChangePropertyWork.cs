using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkMangement.Migrations
{
    public partial class ChangePropertyWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Works",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Works",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

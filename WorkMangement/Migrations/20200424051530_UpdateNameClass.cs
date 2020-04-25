using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkMangement.Migrations
{
    public partial class UpdateNameClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Works_ProjectId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Works");

            migrationBuilder.AddColumn<string>(
                name: "WorkDescription",
                table: "Works",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    PhaseId = table.Column<Guid>(nullable: false),
                    PhaseName = table.Column<string>(nullable: true),
                    WorkId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.PhaseId);
                    table.ForeignKey(
                        name: "FK_Phases_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phases_WorkId",
                table: "Phases",
                column: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropColumn(
                name: "WorkDescription",
                table: "Works");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Works",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Works",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_ProjectId",
                table: "Works",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Projects_ProjectId",
                table: "Works",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

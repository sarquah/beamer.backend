using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementAPI.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Projects");

            migrationBuilder.AddColumn<long>(
                name: "TaskOwnerId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectOwnerId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskOwnerId",
                table: "Tasks",
                column: "TaskOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectOwnerId",
                table: "Projects",
                column: "ProjectOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_ProjectOwnerId",
                table: "Projects",
                column: "ProjectOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_TaskOwnerId",
                table: "Tasks",
                column: "TaskOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ProjectOwnerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_TaskOwnerId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskOwnerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectOwnerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TaskOwnerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectOwnerId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Projects",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IssueWebApp.Migrations
{
    public partial class UpdateUserBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bio_Users_UserId1",
                table: "Bio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bio",
                table: "Bio");

            migrationBuilder.DropIndex(
                name: "IX_Bio_UserId1",
                table: "Bio");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Bio");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bio",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserBioId",
                table: "Bio",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bio",
                table: "Bio",
                column: "UserBioId");

            migrationBuilder.CreateIndex(
                name: "IX_Bio_UserId",
                table: "Bio",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bio_Users_UserId",
                table: "Bio",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bio_Users_UserId",
                table: "Bio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bio",
                table: "Bio");

            migrationBuilder.DropIndex(
                name: "IX_Bio_UserId",
                table: "Bio");

            migrationBuilder.DropColumn(
                name: "UserBioId",
                table: "Bio");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bio",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Bio",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bio",
                table: "Bio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bio_UserId1",
                table: "Bio",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bio_Users_UserId1",
                table: "Bio",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

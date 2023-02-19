using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace IssueWebApp.Migrations
{
   public partial class InitDatabase : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Divisions",
             columns: table => new
             {
                DivisionId = table.Column<int>(type: "integer", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Divisions", x => x.DivisionId);
             });

         migrationBuilder.CreateTable(
             name: "Issues",
             columns: table => new
             {
                Id = table.Column<int>(type: "integer", nullable: false)
                     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Title = table.Column<string>(type: "text", nullable: false),
                OverdueFlag = table.Column<string>(type: "text", nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                RaisedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                DivisionId = table.Column<int>(type: "integer", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Issues", x => x.Id);
                table.ForeignKey(
                       name: "FK_Issues_Divisions_DivisionId",
                       column: x => x.DivisionId,
                       principalTable: "Divisions",
                       principalColumn: "DivisionId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_Issues_DivisionId",
             table: "Issues",
             column: "DivisionId");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "Issues");

         migrationBuilder.DropTable(
             name: "Divisions");
      }
   }
}
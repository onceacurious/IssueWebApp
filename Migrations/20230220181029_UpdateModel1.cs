using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueWebApp.Migrations
{
   public partial class UpdateModel1 : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_Comments_Answers_AnswerId",
             table: "Comments");

         migrationBuilder.AlterColumn<int>(
             name: "AnswerId",
             table: "Comments",
             type: "integer",
             nullable: false,
             defaultValue: 0,
             oldClrType: typeof(int),
             oldType: "integer",
             oldNullable: true);

         migrationBuilder.AddForeignKey(
             name: "FK_Comments_Answers_AnswerId",
             table: "Comments",
             column: "AnswerId",
             principalTable: "Answers",
             principalColumn: "AnswerId",
             onDelete: ReferentialAction.Cascade);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
             name: "FK_Comments_Answers_AnswerId",
             table: "Comments");

         migrationBuilder.AlterColumn<int>(
             name: "AnswerId",
             table: "Comments",
             type: "integer",
             nullable: true,
             oldClrType: typeof(int),
             oldType: "integer");

         migrationBuilder.AddForeignKey(
             name: "FK_Comments_Answers_AnswerId",
             table: "Comments",
             column: "AnswerId",
             principalTable: "Answers",
             principalColumn: "AnswerId",
             onDelete: ReferentialAction.Restrict);
      }
   }
}
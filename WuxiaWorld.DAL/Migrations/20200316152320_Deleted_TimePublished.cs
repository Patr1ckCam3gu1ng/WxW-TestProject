using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WuxiaWorld.DAL.Migrations
{
    public partial class Deleted_TimePublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePublished",
                table: "Novels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimePublished",
                table: "Novels",
                type: "datetime2",
                nullable: true);
        }
    }
}

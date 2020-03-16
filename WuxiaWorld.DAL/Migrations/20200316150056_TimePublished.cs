using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WuxiaWorld.DAL.Migrations
{
    public partial class TimePublished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedOn",
                table: "Novels");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePublished",
                table: "Novels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePublished",
                table: "Novels");

            migrationBuilder.AddColumn<bool>(
                name: "PublishedOn",
                table: "Novels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

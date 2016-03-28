using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace LogGrabber.Web.Migrations
{
    public partial class UniqueErrorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Error",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Name", table: "Error");
        }
    }
}

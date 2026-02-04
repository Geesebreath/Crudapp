using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crudman.Migrations
{
    /// <inheritdoc />
    public partial class statusandtimeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "URLModel",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "URLModel",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCode",
                table: "URLModel",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "URLModel");

            migrationBuilder.DropColumn(
                name: "TimeCode",
                table: "URLModel");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "URLModel",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}

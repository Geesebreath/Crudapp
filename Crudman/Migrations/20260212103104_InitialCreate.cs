using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crudman.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectionType = table.Column<int>(type:"INTEGER", nullable: true),
                    StatusCode = table.Column<int>(type:"INTEGER", nullable: true),
                    TimeCode = table.Column<string>(type:"TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DropTable(
                name: "UrlModel");
        }
    }
}

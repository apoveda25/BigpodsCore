using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigpods.Monolith.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AddColumn<string>(
                    name: "Comment",
                    table: "inventory_outputs",
                    type: "varchar(255)",
                    nullable: false,
                    defaultValue: ""
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .AddColumn<string>(
                    name: "Comment",
                    table: "inventory_inputs",
                    type: "varchar(255)",
                    nullable: false,
                    defaultValue: ""
                )
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Comment", table: "inventory_outputs");

            migrationBuilder.DropColumn(name: "Comment", table: "inventory_inputs");
        }
    }
}

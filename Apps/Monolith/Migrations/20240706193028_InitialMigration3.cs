using System;
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
            migrationBuilder.DropTable(
                name: "medias_on_variants");

            migrationBuilder.CreateTable(
                name: "variants_on_medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    MediaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    CreatedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "utf8mb4_general_ci"),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variants_on_medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_variants_on_medias_medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_variants_on_medias_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_variants_on_medias_MediaId",
                table: "variants_on_medias",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_on_medias_VariantId_MediaId",
                table: "variants_on_medias",
                columns: new[] { "VariantId", "MediaId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "variants_on_medias");

            migrationBuilder.CreateTable(
                name: "medias_on_variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    CreatedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    DeletedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "utf8mb4_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    MediaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    UpdatedAtDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAtTimezone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias_on_variants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_medias_on_variants_VariantId_MediaId",
                table: "medias_on_variants",
                columns: new[] { "VariantId", "MediaId" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigpods.Monolith.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attribute_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValuePattern = table.Column<string>(type: "text(65535)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MeasuringUnitPattern = table.Column<string>(type: "text(65535)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_attribute_types", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<int>(type: "int(2)", nullable: false),
                    ContentType = table.Column<string>(type: "varchar(127)", maxLength: 127, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "varchar(63)", maxLength: 63, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_medias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    table.PrimaryKey("PK_warehouses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Value = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MeasuringUnit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AttributeTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attributes_attribute_types_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "attribute_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sku = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_variants_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Stock = table.Column<int>(type: "int(11)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventories_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventories_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventories_warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "variants_on_attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    AttributeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_variants_on_attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_variants_on_attributes_attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_variants_on_attributes_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "inventory_inputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Stock = table.Column<int>(type: "int(11)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    InventoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_inventory_inputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventory_inputs_inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_inputs_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_inputs_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_inputs_warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inventory_outputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    Stock = table.Column<int>(type: "int(11)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    VariantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    WarehouseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
                    InventoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "utf8mb4_general_ci"),
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
                    table.PrimaryKey("PK_inventory_outputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventory_outputs_inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_outputs_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_outputs_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_outputs_warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_types_Name",
                table: "attribute_types",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_AttributeTypeId",
                table: "attributes",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_Value_MeasuringUnit_AttributeTypeId",
                table: "attributes",
                columns: new[] { "Value", "MeasuringUnit", "AttributeTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_inventories_ProductId",
                table: "inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_VariantId_WarehouseId",
                table: "inventories",
                columns: new[] { "VariantId", "WarehouseId" });

            migrationBuilder.CreateIndex(
                name: "IX_inventories_WarehouseId",
                table: "inventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_inputs_InventoryId",
                table: "inventory_inputs",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_inputs_ProductId",
                table: "inventory_inputs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_inputs_VariantId",
                table: "inventory_inputs",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_inputs_WarehouseId",
                table: "inventory_inputs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outputs_InventoryId",
                table: "inventory_outputs",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outputs_ProductId",
                table: "inventory_outputs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outputs_VariantId",
                table: "inventory_outputs",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outputs_WarehouseId",
                table: "inventory_outputs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_medias_ContentType",
                table: "medias",
                column: "ContentType");

            migrationBuilder.CreateIndex(
                name: "IX_medias_Extension",
                table: "medias",
                column: "Extension");

            migrationBuilder.CreateIndex(
                name: "IX_medias_Path",
                table: "medias",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_products_Brand",
                table: "products",
                column: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_products_Model",
                table: "products",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_variants_ProductId",
                table: "variants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_Sku",
                table: "variants",
                column: "Sku");

            migrationBuilder.CreateIndex(
                name: "IX_variants_on_attributes_AttributeId",
                table: "variants_on_attributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_on_attributes_VariantId_AttributeId",
                table: "variants_on_attributes",
                columns: new[] { "VariantId", "AttributeId" });

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
                name: "inventory_inputs");

            migrationBuilder.DropTable(
                name: "inventory_outputs");

            migrationBuilder.DropTable(
                name: "variants_on_attributes");

            migrationBuilder.DropTable(
                name: "variants_on_medias");

            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "variants");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "attribute_types");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}

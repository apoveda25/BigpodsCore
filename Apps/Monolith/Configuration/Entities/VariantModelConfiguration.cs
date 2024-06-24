using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigpods.Monolith.Config.Entities;

public class VariantModelConfiguration : IEntityTypeConfiguration<VariantModel>
{
    public void Configure(EntityTypeBuilder<VariantModel> builder)
    {
        builder.ToTable(name: "variants");

        builder.HasKey(keyExpression: x => x.Id);

        builder.Property(propertyExpression: x => x.Id).UseCollation(collation: "utf8mb4_general_ci");

        builder.Property(propertyExpression: x => x.Name).IsRequired().HasMaxLength(maxLength: 255);

        builder.Property(propertyExpression: x => x.Sku).IsRequired().HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.Price).IsRequired().HasColumnType("decimal(10,2)");

        builder.Property(propertyExpression: x => x.Cost).IsRequired().HasColumnType("decimal(10,2)");

        builder.Property(propertyExpression: x => x.Stock).IsRequired().HasColumnType("int(11)");

        builder.Property(propertyExpression: x => x.IsDeleted).IsRequired().HasDefaultValue(value: false);

        builder.Property(propertyExpression: x => x.CreatedAtDatetime).IsRequired();

        builder.Property(propertyExpression: x => x.UpdatedAtDatetime);

        builder.Property(propertyExpression: x => x.DeletedAtDatetime);

        builder.Property(propertyExpression: x => x.CreatedAtTimezone).IsRequired().HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.UpdatedAtTimezone).HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.DeletedAtTimezone).HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.CreatedBy).IsRequired().UseCollation(collation: "utf8mb4_general_ci");

        builder.Property(propertyExpression: x => x.UpdatedBy).UseCollation(collation: "utf8mb4_general_ci");

        builder.Property(propertyExpression: x => x.DeletedBy).UseCollation(collation: "utf8mb4_general_ci");

        builder.Property(propertyExpression: x => x.ProductId).UseCollation(collation: "utf8mb4_general_ci");

        builder.HasIndex(indexExpression: p => p.Sku);
    }
}
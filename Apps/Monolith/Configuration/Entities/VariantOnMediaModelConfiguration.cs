using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigpods.Monolith.Configuration.Entities;

public class MediaOnVariantModelConfiguration : IEntityTypeConfiguration<VariantOnMediaModel>
{
    public void Configure(EntityTypeBuilder<VariantOnMediaModel> builder)
    {
        builder.ToTable(name: "variants_on_medias");

        builder.HasKey(keyExpression: x => x.Id);

        builder
            .Property(propertyExpression: x => x.Id)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder
            .Property(propertyExpression: x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(value: false);

        builder.Property(propertyExpression: x => x.CreatedAtDatetime).IsRequired();

        builder.Property(propertyExpression: x => x.UpdatedAtDatetime);

        builder.Property(propertyExpression: x => x.DeletedAtDatetime);

        builder
            .Property(propertyExpression: x => x.CreatedAtTimezone)
            .IsRequired()
            .HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.UpdatedAtTimezone).HasMaxLength(maxLength: 36);

        builder.Property(propertyExpression: x => x.DeletedAtTimezone).HasMaxLength(maxLength: 36);

        builder
            .Property(propertyExpression: x => x.CreatedBy)
            .IsRequired()
            .UseCollation(collation: "utf8mb4_general_ci");

        builder
            .Property(propertyExpression: x => x.UpdatedBy)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder
            .Property(propertyExpression: x => x.DeletedBy)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder
            .Property(propertyExpression: x => x.VariantId)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder
            .Property(propertyExpression: x => x.MediaId)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder.HasIndex(indexExpression: p => new { p.VariantId, p.MediaId });
    }
}

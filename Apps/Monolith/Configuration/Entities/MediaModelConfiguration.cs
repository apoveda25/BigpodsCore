using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigpods.Monolith.Configuration.Entities;

public class MediaModelConfiguration : IEntityTypeConfiguration<MediaModel>
{
    public void Configure(EntityTypeBuilder<MediaModel> builder)
    {
        builder.ToTable(name: "medias");

        builder.HasKey(keyExpression: x => x.Id);

        builder
            .Property(propertyExpression: x => x.Id)
            .UseCollation(collation: "utf8mb4_general_ci");

        builder.Property(propertyExpression: x => x.Path).IsRequired().HasMaxLength(maxLength: 255);

        builder.Property(propertyExpression: x => x.Url).IsRequired().HasMaxLength(maxLength: 255);

        builder.Property(propertyExpression: x => x.Order).IsRequired().HasColumnType("int(2)");

        builder
            .Property(propertyExpression: x => x.ContentType)
            .IsRequired()
            .HasMaxLength(maxLength: 127);

        builder
            .Property(propertyExpression: x => x.Extension)
            .IsRequired()
            .HasMaxLength(maxLength: 63);

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

        builder.HasIndex(indexExpression: p => p.Path);

        builder.HasIndex(indexExpression: p => p.ContentType);

        builder.HasIndex(indexExpression: p => p.Extension);
    }
}

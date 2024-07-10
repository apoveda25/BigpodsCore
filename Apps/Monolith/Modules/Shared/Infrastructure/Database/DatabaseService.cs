using System.Reflection;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Database;

public class DatabaseService(DbContextOptions dbContextOptions)
    : DbContext(options: dbContextOptions)
{
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<VariantModel> Variants { get; set; }
    public DbSet<VariantOnAttributeModel> VariantsOnAttributes { get; set; }
    public DbSet<AttributeModel> Attributes { get; set; }
    public DbSet<AttributeTypeModel> AttributeTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
    }
}

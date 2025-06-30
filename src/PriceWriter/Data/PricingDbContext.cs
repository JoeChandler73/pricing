using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace PriceWriter.Data;

public sealed class PricingDbContext : DbContext
{
    public PricingDbContext(DbContextOptions options) : base(options)
    {
        if (Database.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator) 
            return;
        
        if(!databaseCreator.CanConnect())
            databaseCreator.Create();
            
        if(!databaseCreator.HasTables())
            databaseCreator.CreateTables();
    }
    
    public DbSet<PriceData> Prices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PriceEntityTypeConfiguration());
    }
}
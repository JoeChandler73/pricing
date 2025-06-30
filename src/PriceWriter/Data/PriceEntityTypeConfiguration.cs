using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PriceWriter.Data;

public class PriceEntityTypeConfiguration : IEntityTypeConfiguration<PriceData>
{
    public void Configure(EntityTypeBuilder<PriceData> builder)
    {
        builder.ToTable("Price", "dbo");

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Symbol).IsRequired();
        builder.Property(x => x.Mid).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Timestamp).IsRequired();

        builder.HasKey(x => x.Id);
    }
}
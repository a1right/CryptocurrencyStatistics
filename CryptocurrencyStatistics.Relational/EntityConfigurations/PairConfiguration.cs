
using CryptocurrencyStatistics.RelationalStorage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptocurrencyStatistics.RelationalStorage.EntityConfigurations
{
    public class PairConfiguration : IEntityTypeConfiguration<Pair>
    {
        public void Configure(EntityTypeBuilder<Pair> builder)
        {
            builder.ToTable("Pair");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("id");
            
            builder.Property(p => p.Name)
                   .HasColumnName("pair_name")
                   .HasMaxLength(50);
            
            builder.HasIndex(p => p.Name)
                   .IsUnique()
                   .HasDatabaseName("uk_pair_name");

            builder.HasMany(p => p.StatisticDatas)
                   .WithOne(s => s.Pair);
        }
    }
}

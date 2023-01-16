using CryptocurrencyStatistics.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptocurrencyStatistics.Relational.EntityConfigurations
{
    internal class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.ToTable("Record");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.PairName).HasColumnName("pair_name").HasMaxLength(50);
            builder.Property(r => r.Value).HasColumnName("value").HasColumnType("decimal(13,8)");
            builder.Property(r => r.CreatedDateTime).HasColumnName("created_datetime");
            builder.HasIndex(r => r.CreatedDateTime).HasDatabaseName("ix_record_created_datetime");
        }
    }
}

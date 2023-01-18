using System;
using System.Collections.Generic;
using System.Text;
using CryptocurrencyStatistics.RelationalStorage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptocurrencyStatistics.RelationalStorage.EntityConfigurations
{
    public class StatisticDataConfiguration : IEntityTypeConfiguration<StatisticData>
    {
        public void Configure(EntityTypeBuilder<StatisticData> builder)
        {
            builder.ToTable("Statistic_data");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnName("id");
            
            builder.Property(s => s.Value)
                   .HasColumnName("value")
                   .HasColumnType("decimal(13,8)");
            
            builder.Property(s => s.CreatedDateTime)
                   .HasColumnName("created_datetime");
            
            builder.HasIndex(s => s.CreatedDateTime)
                   .HasDatabaseName("ix_record_created_datetime");
            
            builder.HasOne(s => s.Pair)
                   .WithMany(p => p.StatisticDatas)
                   .HasForeignKey(p => p.PairId);
        }
    }
}

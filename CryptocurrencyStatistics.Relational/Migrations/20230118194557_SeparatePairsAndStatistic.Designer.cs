﻿// <auto-generated />
using System;
using CryptocurrencyStatistics.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CryptocurrencyStatistics.RelationalStorage.Migrations
{
    [DbContext(typeof(RecordsDbContext))]
    [Migration("20230118194557_SeparatePairsAndStatistic")]
    partial class SeparatePairsAndStatistic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CryptocurrencyStatistics.RelationalStorage.Models.Pair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("pair_name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("uk_pair_name");

                    b.ToTable("Pair");
                });

            modelBuilder.Entity("CryptocurrencyStatistics.RelationalStorage.Models.StatisticData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_datetime");

                    b.Property<int>("PairId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric(13,8)")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDateTime")
                        .HasDatabaseName("ix_record_created_datetime");

                    b.HasIndex("PairId");

                    b.ToTable("Statistic_data");
                });

            modelBuilder.Entity("CryptocurrencyStatistics.RelationalStorage.Models.StatisticData", b =>
                {
                    b.HasOne("CryptocurrencyStatistics.RelationalStorage.Models.Pair", "Pair")
                        .WithMany("StatisticDatas")
                        .HasForeignKey("PairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pair");
                });

            modelBuilder.Entity("CryptocurrencyStatistics.RelationalStorage.Models.Pair", b =>
                {
                    b.Navigation("StatisticDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
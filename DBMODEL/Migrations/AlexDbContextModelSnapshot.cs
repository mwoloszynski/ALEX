﻿// <auto-generated />
using DBMODEL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBMODEL.Migrations
{
    [DbContext(typeof(AlexDbContext))]
    partial class AlexDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DBMODEL.Entities.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryISO2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryISO3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperatingMIC")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("DBMODEL.Entities.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExchangeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExchangeId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("DBMODEL.Entities.StockResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.Property<decimal>("change")
                        .HasColumnType("numeric");

                    b.Property<double>("change_p")
                        .HasColumnType("double precision");

                    b.Property<decimal>("close")
                        .HasColumnType("numeric");

                    b.Property<int>("gmtoffset")
                        .HasColumnType("integer");

                    b.Property<decimal>("high")
                        .HasColumnType("numeric");

                    b.Property<decimal>("low")
                        .HasColumnType("numeric");

                    b.Property<decimal>("open")
                        .HasColumnType("numeric");

                    b.Property<decimal>("previousClose")
                        .HasColumnType("numeric");

                    b.Property<long>("timestamp")
                        .HasColumnType("bigint");

                    b.Property<decimal>("volume")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("StockResults");
                });

            modelBuilder.Entity("DBMODEL.Entities.Stock", b =>
                {
                    b.HasOne("DBMODEL.Entities.Exchange", "Exchange")
                        .WithMany()
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exchange");
                });

            modelBuilder.Entity("DBMODEL.Entities.StockResult", b =>
                {
                    b.HasOne("DBMODEL.Entities.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SC.DevChallenge.Db.Contexts;

namespace SC.DevChallenge.Db.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SC.DevChallenge.Db.Models.ContentHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Hash")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdate");

                    b.HasKey("Id");

                    b.ToTable("ContentHistories");
                });

            modelBuilder.Entity("SC.DevChallenge.Db.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("SC.DevChallenge.Db.Models.InstrumentOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("InstrumentOwners");
                });

            modelBuilder.Entity("SC.DevChallenge.Db.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("SC.DevChallenge.Db.Models.PriceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("InstrumentId");

                    b.Property<int>("InstrumentOwnerId");

                    b.Property<int>("PortfolioId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("InstrumentOwnerId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PriceModels");
                });

            modelBuilder.Entity("SC.DevChallenge.Db.Models.PriceModel", b =>
                {
                    b.HasOne("SC.DevChallenge.Db.Models.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.Db.Models.InstrumentOwner", "InstrumentOwner")
                        .WithMany()
                        .HasForeignKey("InstrumentOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SC.DevChallenge.Db.Models.Portfolio", "Portfolio")
                        .WithMany()
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

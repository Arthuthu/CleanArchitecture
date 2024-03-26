﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionInfra.Context;

#nullable disable

namespace SubscriptionInfra.Migrations
{
    [DbContext(typeof(SubscriptionContext))]
    partial class SubscriptionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SubscriptionDomain.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId")
                        .IsUnique()
                        .HasFilter("[SubscriptionId] IS NOT NULL");

                    b.ToTable("Contracts", (string)null);
                });

            modelBuilder.Entity("SubscriptionDomain.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("MonthlyPrice")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions", (string)null);
                });

            modelBuilder.Entity("SubscriptionDomain.Entities.Contract", b =>
                {
                    b.HasOne("SubscriptionDomain.Entities.Subscription", "Subscription")
                        .WithOne("Contract")
                        .HasForeignKey("SubscriptionDomain.Entities.Contract", "SubscriptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("SubscriptionDomain.Entities.Subscription", b =>
                {
                    b.Navigation("Contract");
                });
#pragma warning restore 612, 618
        }
    }
}

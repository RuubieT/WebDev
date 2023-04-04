﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebDevAPI.Db;

#nullable disable

namespace WebDevAPI.Migrations
{
    [DbContext(typeof(WebDevDbContext))]
    partial class WebDevDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebDevAPI.Db.Models.Card", b =>
                {
                    b.Property<Guid>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("InHand")
                        .HasColumnType("bit");

                    b.Property<int>("MySuit")
                        .HasColumnType("int");

                    b.Property<int>("MyValue")
                        .HasColumnType("int");

                    b.HasKey("CardId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.Contactform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contactforms");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.PlayerHand", b =>
                {
                    b.Property<Guid>("PlayerHandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FirstCardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SecondCardId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlayerHandId");

                    b.HasIndex("FirstCardId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.HasIndex("SecondCardId");

                    b.ToTable("PlayerHands");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.PokerTable", b =>
                {
                    b.Property<Guid>("PokerTableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Ante")
                        .HasColumnType("int");

                    b.Property<int>("BigBlind")
                        .HasColumnType("int");

                    b.Property<int>("MaxSeats")
                        .HasColumnType("int");

                    b.Property<int>("SmallBlind")
                        .HasColumnType("int");

                    b.HasKey("PokerTableId");

                    b.ToTable("PokerTables");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.Player", b =>
                {
                    b.HasBaseType("WebDevAPI.Db.Models.User");

                    b.Property<int>("Chips")
                        .HasColumnType("int");

                    b.Property<Guid?>("PokerTableId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("PokerTableId");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.PlayerHand", b =>
                {
                    b.HasOne("WebDevAPI.Db.Models.Card", "FirstCard")
                        .WithMany()
                        .HasForeignKey("FirstCardId");

                    b.HasOne("WebDevAPI.Db.Models.Player", "Player")
                        .WithOne("PlayerHand")
                        .HasForeignKey("WebDevAPI.Db.Models.PlayerHand", "PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WebDevAPI.Db.Models.Card", "SecondCard")
                        .WithMany()
                        .HasForeignKey("SecondCardId");

                    b.Navigation("FirstCard");

                    b.Navigation("Player");

                    b.Navigation("SecondCard");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.Player", b =>
                {
                    b.HasOne("WebDevAPI.Db.Models.PokerTable", "PokerTable")
                        .WithMany("Players")
                        .HasForeignKey("PokerTableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PokerTable");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.PokerTable", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("WebDevAPI.Db.Models.Player", b =>
                {
                    b.Navigation("PlayerHand")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

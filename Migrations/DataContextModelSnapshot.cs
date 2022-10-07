﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealPetApi.Data;

#nullable disable

namespace RealPetApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RealPetApi.Models.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("RealPetApi.Models.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Founded")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("RealPetApi.Models.ClubSport", b =>
                {
                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.HasKey("ClubId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("ClubSports");
                });

            modelBuilder.Entity("RealPetApi.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HandlerId")
                        .HasColumnType("int");

                    b.Property<int>("WallPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.HasIndex("WallPostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RealPetApi.Models.Dog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<int>("HandlerId")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("HandlerId");

                    b.HasIndex("LocationId");

                    b.ToTable("Dogs");
                });

            modelBuilder.Entity("RealPetApi.Models.DogSport", b =>
                {
                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.HasKey("DogId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("DogSports");
                });

            modelBuilder.Entity("RealPetApi.Models.DogTitle", b =>
                {
                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("DogId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("DogTitles");
                });

            modelBuilder.Entity("RealPetApi.Models.Handler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Handlers");
                });

            modelBuilder.Entity("RealPetApi.Models.HandlerSport", b =>
                {
                    b.Property<int>("HandlerId")
                        .HasColumnType("int");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.HasKey("HandlerId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("HandlerSports");
                });

            modelBuilder.Entity("RealPetApi.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("RealPetApi.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("RealPetApi.Models.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("RealPetApi.Models.WallPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wallposts");
                });

            modelBuilder.Entity("RealPetApi.Models.Club", b =>
                {
                    b.HasOne("RealPetApi.Models.Location", "Location")
                        .WithMany("Clubs")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("RealPetApi.Models.ClubSport", b =>
                {
                    b.HasOne("RealPetApi.Models.Club", "Club")
                        .WithMany("ClubSports")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Sport", "Sport")
                        .WithMany("ClubSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("RealPetApi.Models.Comment", b =>
                {
                    b.HasOne("RealPetApi.Models.Handler", "Handler")
                        .WithMany()
                        .HasForeignKey("HandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.WallPost", "WallPost")
                        .WithMany()
                        .HasForeignKey("WallPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handler");

                    b.Navigation("WallPost");
                });

            modelBuilder.Entity("RealPetApi.Models.Dog", b =>
                {
                    b.HasOne("RealPetApi.Models.Breed", "Breed")
                        .WithMany("Dogs")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Handler", "Handler")
                        .WithMany("Dogs")
                        .HasForeignKey("HandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Location", "Location")
                        .WithMany("Dogs")
                        .HasForeignKey("LocationId");

                    b.Navigation("Breed");

                    b.Navigation("Handler");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("RealPetApi.Models.DogSport", b =>
                {
                    b.HasOne("RealPetApi.Models.Dog", "Dog")
                        .WithMany("DogSports")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Sport", "Sport")
                        .WithMany("DogSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("RealPetApi.Models.DogTitle", b =>
                {
                    b.HasOne("RealPetApi.Models.Dog", "Dog")
                        .WithMany("DogTitles")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Title", "Title")
                        .WithMany("DogTitles")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("RealPetApi.Models.Handler", b =>
                {
                    b.HasOne("RealPetApi.Models.Location", "Location")
                        .WithMany("Handlers")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("RealPetApi.Models.HandlerSport", b =>
                {
                    b.HasOne("RealPetApi.Models.Handler", "Handler")
                        .WithMany("HandlerSports")
                        .HasForeignKey("HandlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealPetApi.Models.Sport", "Sport")
                        .WithMany("HandlerSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handler");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("RealPetApi.Models.Breed", b =>
                {
                    b.Navigation("Dogs");
                });

            modelBuilder.Entity("RealPetApi.Models.Club", b =>
                {
                    b.Navigation("ClubSports");
                });

            modelBuilder.Entity("RealPetApi.Models.Dog", b =>
                {
                    b.Navigation("DogSports");

                    b.Navigation("DogTitles");
                });

            modelBuilder.Entity("RealPetApi.Models.Handler", b =>
                {
                    b.Navigation("Dogs");

                    b.Navigation("HandlerSports");
                });

            modelBuilder.Entity("RealPetApi.Models.Location", b =>
                {
                    b.Navigation("Clubs");

                    b.Navigation("Dogs");

                    b.Navigation("Handlers");
                });

            modelBuilder.Entity("RealPetApi.Models.Sport", b =>
                {
                    b.Navigation("ClubSports");

                    b.Navigation("DogSports");

                    b.Navigation("HandlerSports");
                });

            modelBuilder.Entity("RealPetApi.Models.Title", b =>
                {
                    b.Navigation("DogTitles");
                });
#pragma warning restore 612, 618
        }
    }
}

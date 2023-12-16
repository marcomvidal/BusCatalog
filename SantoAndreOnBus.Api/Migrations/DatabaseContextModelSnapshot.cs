﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SantoAndreOnBus.Api.Infrastructure;

#nullable disable

namespace SantoAndreOnBus.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("LinePlace", b =>
                {
                    b.Property<int>("LinesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlacesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LinesId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("LinePlace");
                });

            modelBuilder.Entity("LineVehicle", b =>
                {
                    b.Property<int>("LinesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VehiclesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LinesId", "VehiclesId");

                    b.HasIndex("VehiclesId");

                    b.ToTable("LineVehicle");
                });

            modelBuilder.Entity("SantoAndreOnBus.Api.Business.Lines.Line", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeparturesPerDay")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fromwards")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Towards")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("SantoAndreOnBus.Api.Business.Places.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("SantoAndreOnBus.Api.Business.Vehicles.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("LinePlace", b =>
                {
                    b.HasOne("SantoAndreOnBus.Api.Business.Lines.Line", null)
                        .WithMany()
                        .HasForeignKey("LinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SantoAndreOnBus.Api.Business.Places.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LineVehicle", b =>
                {
                    b.HasOne("SantoAndreOnBus.Api.Business.Lines.Line", null)
                        .WithMany()
                        .HasForeignKey("LinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SantoAndreOnBus.Api.Business.Vehicles.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehiclesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
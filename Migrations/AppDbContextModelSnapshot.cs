﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockControl.Data;

#nullable disable

namespace Stock_Control.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("StockControl.Models.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("StockControl.Models.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MachineId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("StockControl.Models.Part", b =>
                {
                    b.HasOne("StockControl.Models.Machine", "Machine")
                        .WithMany("Parts")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("StockControl.Models.Machine", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}

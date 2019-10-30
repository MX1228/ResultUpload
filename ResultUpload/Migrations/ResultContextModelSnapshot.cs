﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResultUpload.Models;

namespace ResultUpload.Migrations
{
    [DbContext(typeof(ResultContext))]
    partial class ResultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ResultUpload.Models.Result", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachmet");

                    b.Property<DateTime>("Create");

                    b.Property<string>("Discription")
                        .IsRequired();

                    b.Property<string>("PassWord");

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("TID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("TID");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("ResultUpload.Models.ResultType", b =>
                {
                    b.Property<int>("TID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("TID");

                    b.ToTable("ResultTypes");
                });

            modelBuilder.Entity("ResultUpload.Models.Result", b =>
                {
                    b.HasOne("ResultUpload.Models.ResultType", "Type")
                        .WithMany("Results")
                        .HasForeignKey("TID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190719084838_Status")]
    partial class Status
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectManagementAPI.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<long?>("OwnerId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementAPI.Models.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<long?>("OwnerId");

                    b.Property<long?>("ProjectId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectManagementAPI.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Department");

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManagementAPI.Models.Project", b =>
                {
                    b.HasOne("ProjectManagementAPI.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("ProjectManagementAPI.Models.Task", b =>
                {
                    b.HasOne("ProjectManagementAPI.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("ProjectManagementAPI.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}

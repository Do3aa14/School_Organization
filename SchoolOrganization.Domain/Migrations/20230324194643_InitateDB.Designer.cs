﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolOrganization.Domain.Context;

namespace SchoolOrganization.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230324194643_InitateDB")]
    partial class InitateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolOrganization.Domain.Entities.School", b =>
                {
                    b.Property<int>("School_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("School_Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("School_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("School_Id");

                    b.ToTable("School");
                });
#pragma warning restore 612, 618
        }
    }
}

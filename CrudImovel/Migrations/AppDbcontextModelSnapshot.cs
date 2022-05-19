﻿// <auto-generated />
using CrudImovel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrudImovel.Migrations
{
    [DbContext(typeof(AppDbcontext))]
    partial class AppDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("CrudImovel.Data.Imovel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext");

                    b.Property<string>("Rua")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Imovel");
                });
#pragma warning restore 612, 618
        }
    }
}

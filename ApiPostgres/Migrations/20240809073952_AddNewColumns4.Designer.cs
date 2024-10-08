﻿// <auto-generated />
using System;
using ApiPostgres.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiPostgres.Migrations
{
    [DbContext(typeof(Sensors_db))]
    [Migration("20240809073952_AddNewColumns4")]
    partial class AddNewColumns4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiPostgres.Models.Datos_Sensores", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("codigo_parametro")
                        .HasColumnType("integer");

                    b.Property<DateTime>("fecha_dato")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("nombre_parametro")
                        .HasColumnType("text");

                    b.Property<int?>("parametro_sensores_id")
                        .HasColumnType("integer");

                    b.Property<float?>("valor_numero")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.ToTable("datos_sensores");
                });

            modelBuilder.Entity("ApiPostgres.Models.Parametros_Sensores", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("abreviacion")
                        .HasColumnType("text");

                    b.Property<int>("codigo_parametro")
                        .HasColumnType("integer");

                    b.Property<string>("descripcion_corta")
                        .HasColumnType("text");

                    b.Property<string>("descripcion_larga")
                        .HasColumnType("text");

                    b.Property<string>("descripcion_med")
                        .HasColumnType("text");

                    b.Property<string>("estado")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_creacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_modificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("observacion")
                        .HasColumnType("text");

                    b.Property<string>("unidad")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("parametros_sensores");
                });
#pragma warning restore 612, 618
        }
    }
}

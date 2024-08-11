﻿using System;
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
    [Migration("20240809071304_firstMigration2")]
    partial class firstMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiPostgres.Models.datos_sensores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoParametro")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FechaDato")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NombreParametro")
                        .HasColumnType("text");

                    b.Property<int>("ParametroSensorId")
                        .HasColumnType("integer");

                    b.Property<float>("ValorNumero")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("DatosSensores");
                });

            modelBuilder.Entity("ApiPostgres.Models.parametros_sensores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abreviacion")
                        .HasColumnType("text");

                    b.Property<int>("CodigoParametro")
                        .HasColumnType("integer");

                    b.Property<string>("DescripcionCorta")
                        .HasColumnType("text");

                    b.Property<string>("DescripcionLarga")
                        .HasColumnType("text");

                    b.Property<string>("DescripcionMed")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Observacion")
                        .HasColumnType("text");

                    b.Property<string>("Unidad")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ParametrosSensores");
                });
#pragma warning restore 612, 618
        }
    }
}

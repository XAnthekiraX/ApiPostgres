using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiPostgres.Migrations
{
    public partial class AddNewColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosSensores");

            migrationBuilder.DropTable(
                name: "ParametrosSensores");

            migrationBuilder.CreateTable(
                name: "datos_sensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoParametro = table.Column<int>(type: "integer", nullable: false),
                    ParametroSensorId = table.Column<int>(type: "integer", nullable: false),
                    NombreParametro = table.Column<string>(type: "text", nullable: true),
                    FechaDato = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValorNumero = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datos_sensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parametros_sensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoParametro = table.Column<int>(type: "integer", nullable: false),
                    DescripcionLarga = table.Column<string>(type: "text", nullable: true),
                    DescripcionMed = table.Column<string>(type: "text", nullable: true),
                    DescripcionCorta = table.Column<string>(type: "text", nullable: true),
                    Abreviacion = table.Column<string>(type: "text", nullable: true),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Unidad = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametros_sensores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datos_sensores");

            migrationBuilder.DropTable(
                name: "parametros_sensores");

            migrationBuilder.CreateTable(
                name: "DatosSensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoParametro = table.Column<int>(type: "integer", nullable: false),
                    FechaDato = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NombreParametro = table.Column<string>(type: "text", nullable: true),
                    ParametroSensorId = table.Column<int>(type: "integer", nullable: false),
                    ValorNumero = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosSensores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosSensores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Abreviacion = table.Column<string>(type: "text", nullable: true),
                    CodigoParametro = table.Column<int>(type: "integer", nullable: false),
                    DescripcionCorta = table.Column<string>(type: "text", nullable: true),
                    DescripcionLarga = table.Column<string>(type: "text", nullable: true),
                    DescripcionMed = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Unidad = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosSensores", x => x.Id);
                });
        }
    }
}

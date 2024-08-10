using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPostgres.Migrations
{
    public partial class AddNewColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "parametros_sensores");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "parametros_sensores");

            migrationBuilder.DropColumn(
                name: "CodigoParametro",
                table: "datos_sensores");

            migrationBuilder.DropColumn(
                name: "ValorNumero",
                table: "datos_sensores");

            migrationBuilder.RenameColumn(
                name: "Unidad",
                table: "parametros_sensores",
                newName: "unidad");

            migrationBuilder.RenameColumn(
                name: "Observacion",
                table: "parametros_sensores",
                newName: "observacion");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "parametros_sensores",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Abreviacion",
                table: "parametros_sensores",
                newName: "abreviacion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "parametros_sensores",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DescripcionMed",
                table: "parametros_sensores",
                newName: "descripcion_med");

            migrationBuilder.RenameColumn(
                name: "DescripcionLarga",
                table: "parametros_sensores",
                newName: "descripcion_larga");

            migrationBuilder.RenameColumn(
                name: "DescripcionCorta",
                table: "parametros_sensores",
                newName: "descripcion_corta");

            migrationBuilder.RenameColumn(
                name: "CodigoParametro",
                table: "parametros_sensores",
                newName: "codigo_parametro");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "datos_sensores",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ParametroSensorId",
                table: "datos_sensores",
                newName: "codigo_parametro");

            migrationBuilder.RenameColumn(
                name: "NombreParametro",
                table: "datos_sensores",
                newName: "nombre_parametro");

            migrationBuilder.RenameColumn(
                name: "FechaDato",
                table: "datos_sensores",
                newName: "fecha_dato");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "parametros_sensores",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_modificacion",
                table: "parametros_sensores",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parametro_sensores_id",
                table: "datos_sensores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "valor_numero",
                table: "datos_sensores",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "parametros_sensores");

            migrationBuilder.DropColumn(
                name: "fecha_modificacion",
                table: "parametros_sensores");

            migrationBuilder.DropColumn(
                name: "parametro_sensores_id",
                table: "datos_sensores");

            migrationBuilder.DropColumn(
                name: "valor_numero",
                table: "datos_sensores");

            migrationBuilder.RenameColumn(
                name: "unidad",
                table: "parametros_sensores",
                newName: "Unidad");

            migrationBuilder.RenameColumn(
                name: "observacion",
                table: "parametros_sensores",
                newName: "Observacion");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "parametros_sensores",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "abreviacion",
                table: "parametros_sensores",
                newName: "Abreviacion");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "parametros_sensores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "descripcion_med",
                table: "parametros_sensores",
                newName: "DescripcionMed");

            migrationBuilder.RenameColumn(
                name: "descripcion_larga",
                table: "parametros_sensores",
                newName: "DescripcionLarga");

            migrationBuilder.RenameColumn(
                name: "descripcion_corta",
                table: "parametros_sensores",
                newName: "DescripcionCorta");

            migrationBuilder.RenameColumn(
                name: "codigo_parametro",
                table: "parametros_sensores",
                newName: "CodigoParametro");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "datos_sensores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "nombre_parametro",
                table: "datos_sensores",
                newName: "NombreParametro");

            migrationBuilder.RenameColumn(
                name: "fecha_dato",
                table: "datos_sensores",
                newName: "FechaDato");

            migrationBuilder.RenameColumn(
                name: "codigo_parametro",
                table: "datos_sensores",
                newName: "ParametroSensorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "parametros_sensores",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "parametros_sensores",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CodigoParametro",
                table: "datos_sensores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ValorNumero",
                table: "datos_sensores",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}

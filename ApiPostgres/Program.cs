using ApiPostgres.Data;
using ApiPostgres.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<Sensors_db>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/sensores/{id}", async (int id, Sensors_db db) =>
{
    try
    {
        var datos = await db.datos_sensores
            .Join(db.parametros_sensores,
                  ds => ds.parametro_sensores_id,
                  ps => ps.id,
                  (ds, ps) => new
                  {
                      ds.id,
                      ds.codigo_parametro,
                      ps.descripcion_corta,
                      ds.nombre_parametro,
                      ds.fecha_dato,
                      ds.valor_numero,
                      ps.unidad 
                  })
            .Where(d => d.id == id)
            .ToListAsync();

        if (!datos.Any())
        {
            return Results.NotFound("No se encontraron datos para el ID especificado.");
        }

        var groupedData = datos
            .GroupBy(d => d.codigo_parametro)
            .Select(g => new DeviceData
            {
                CodigoParametro = g.Key.ToString(),
                NombreParametro = g.FirstOrDefault()?.nombre_parametro ?? "",
                UnidadParametro = g.FirstOrDefault()?.unidad ?? "",
                AbreviacionParametro = g.FirstOrDefault()?.descripcion_corta?.Substring(0, 4) ?? "",
                Values = new DeviceValues
                {
                    AvgData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Average(d => d.valor_numero.Value) },
                    MinData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Min(d => d.valor_numero.Value) },
                    MaxData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Max(d => d.valor_numero.Value) }
                }
            }).ToList();

        var response = new DeviceDataResponse
        {
            DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
            DeviceData = groupedData
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});

app.MapGet("/sensores/porRangoHoras", async (DateTime fecha, string horaInicio, string horaFin, Sensors_db db) =>
{
    if (!TimeOnly.TryParse(horaInicio, out TimeOnly horaInicioOnly) ||
        !TimeOnly.TryParse(horaFin, out TimeOnly horaFinOnly))
    {
        return Results.BadRequest("Formato de hora inv�lido. Utiliza un formato v�lido como 'HH:MM:SS'.");
    }

    DateTime fechaHoraInicio = fecha.Add(horaInicioOnly.ToTimeSpan());
    DateTime fechaHoraFin = fecha.Add(horaFinOnly.ToTimeSpan());

    try
    {
        var sqlQuery = @"
            SELECT ds.id, ds.codigo_parametro, ps.descripcion_corta, ds.nombre_parametro, ds.fecha_dato, ds.valor_numero, ps.unidad
            FROM datos_sensores ds
            JOIN parametros_sensores ps ON ds.codigo_parametro = ps.codigo_parametro
            WHERE ds.fecha_dato BETWEEN @fechaHoraInicio AND @fechaHoraFin";

        var fechaHoraInicioParam = new NpgsqlParameter("@fechaHoraInicio", fechaHoraInicio);
        var fechaHoraFinParam = new NpgsqlParameter("@fechaHoraFin", fechaHoraFin);

        var datos = await db.SensorDataResults
            .FromSqlRaw(sqlQuery, fechaHoraInicioParam, fechaHoraFinParam)
            .ToListAsync();

        if (datos.Count == 0)
        {
            return Results.NotFound("No se encontraron datos para el rango de horas especificado.");
        }

        var groupedData = datos
            .GroupBy(d => d.codigo_parametro)
            .Select(g => new DeviceData
            {
                CodigoParametro = g.Key.ToString(),
                NombreParametro = g.FirstOrDefault()?.nombre_parametro ?? "",
                UnidadParametro = g.FirstOrDefault()?.unidad ?? "",
                AbreviacionParametro = g.FirstOrDefault()?.descripcion_corta?.Substring(0, 4) ?? "",
                Values = new DeviceValues
                {
                    AvgData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Average(d => d.valor_numero.Value) },
                    MinData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Min(d => d.valor_numero.Value) },
                    MaxData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Max(d => d.valor_numero.Value) }
                }
            }).ToList();

        var response = new DeviceDataResponse
        {
            DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
            DeviceData = groupedData
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");

        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});

app.MapGet("/sensores/porRangoFecha", async (DateTime startDate, DateTime endDate, Sensors_db db) =>
{
    var startDateUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
    var endDateUtc = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

    try
    {
        var sqlQuery = @"
            SELECT ds.id, ds.codigo_parametro, ps.descripcion_corta, ds.nombre_parametro, ds.fecha_dato, ds.valor_numero, ps.unidad
            FROM datos_sensores ds
            JOIN parametros_sensores ps ON ds.codigo_parametro = ps.codigo_parametro
            WHERE  fecha_dato BETWEEN @startDate AND @endDate";

        var dateStartParam = new NpgsqlParameter("@startDate", startDateUtc);
        var dateEndParam = new NpgsqlParameter("@endDate", endDateUtc);

        var datos = await db.SensorDataResults
            .FromSqlRaw(sqlQuery, dateStartParam, dateEndParam)
            .ToListAsync();

        if (datos.Count == 0)
        {
            return Results.NotFound("No se encontraron datos para la hora especificada.");
        }

        var groupedData = datos
            .GroupBy(d => d.codigo_parametro)
            .Select(g => new DeviceData
            {
                CodigoParametro = g.Key.ToString(),
                NombreParametro = g.FirstOrDefault()?.nombre_parametro ?? "",
                UnidadParametro = g.FirstOrDefault()?.unidad ?? "",
                AbreviacionParametro = g.FirstOrDefault()?.descripcion_corta?.Substring(0, 4) ?? "",
                Values = new DeviceValues
                {
                    AvgData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Average(d => d.valor_numero.Value) },
                    MinData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Min(d => d.valor_numero.Value) },
                    MaxData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Max(d => d.valor_numero.Value) }
                }
            }).ToList();

        var response = new DeviceDataResponse
        {
            DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
            DeviceData = groupedData
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");

        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});

app.MapGet("/sensores/porSemana", async (DateTime fechaInicio, Sensors_db db) =>
{
    var fechaInicioUtc = DateTime.SpecifyKind(fechaInicio, DateTimeKind.Utc);
    try
    {
        var sqlQuery = @"
             WITH WeekRange AS (
                SELECT 
                    date_trunc('week', @fechaInicio::date) AS week_start,
                    date_trunc('week', @fechaInicio::date) + INTERVAL '7 days' AS week_end
            )
            SELECT 
                ds.id, 
                ds.codigo_parametro, 
                ps.descripcion_corta, 
                ds.nombre_parametro, 
                ds.fecha_dato, 
                ds.valor_numero, 
                ps.unidad
            FROM 
                datos_sensores ds
            JOIN 
                parametros_sensores ps 
                ON ds.codigo_parametro = ps.codigo_parametro
            JOIN 
                WeekRange wr
                ON ds.fecha_dato >= wr.week_start 
                AND ds.fecha_dato < wr.week_end
            WHERE 
                ds.fecha_dato >= wr.week_start 
                AND ds.fecha_dato < wr.week_end";

        var dateStartParam = new NpgsqlParameter("@fechaInicio", fechaInicioUtc);

        var datos = await db.SensorDataResults
            .FromSqlRaw(sqlQuery, dateStartParam)
            .ToListAsync();

        if (datos.Count == 0)
        {
            return Results.NotFound("No se encontraron datos para la hora especificada.");
        }

        var groupedData = datos
            .GroupBy(d => d.codigo_parametro)
            .Select(g => new DeviceData
            {
                CodigoParametro = g.Key.ToString(),
                NombreParametro = g.FirstOrDefault()?.nombre_parametro ?? "",
                UnidadParametro = g.FirstOrDefault()?.unidad ?? "",
                AbreviacionParametro = g.FirstOrDefault()?.descripcion_corta?.Substring(0, 4) ?? "",
                Values = new DeviceValues
                {
                    AvgData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Average(d => d.valor_numero.Value) },
                    MinData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Min(d => d.valor_numero.Value) },
                    MaxData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Max(d => d.valor_numero.Value) }
                }
            }).ToList();

        var response = new DeviceDataResponse
        {
            DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
            DeviceData = groupedData
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");

        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});

app.MapGet("/sensores/porMes", async (DateTime fechaInicio, DateTime fechaFin, Sensors_db db) =>
{
    try
    {
        var sqlQuery = @"
            SELECT 
                ds.id, 
                ds.codigo_parametro, 
                ps.descripcion_corta, 
                ds.nombre_parametro, 
                ds.fecha_dato, 
                ds.valor_numero, 
                ps.unidad
            FROM 
                datos_sensores ds
            JOIN 
                parametros_sensores ps 
                ON ds.codigo_parametro = ps.codigo_parametro
            WHERE 
                ds.fecha_dato >= date_trunc('month', @fechaInicio)
                AND ds.fecha_dato < date_trunc('month', @fechaFin) + INTERVAL '1 month'";

        var fechaInicioParam = new NpgsqlParameter("@fechaInicio", fechaInicio);
        var fechaFinParam = new NpgsqlParameter("@fechaFin", fechaFin);

        var datos = await db.SensorDataResults
            .FromSqlRaw(sqlQuery, fechaInicioParam, fechaFinParam)
            .ToListAsync();

        if (datos.Count == 0)
        {
            return Results.NotFound("No se encontraron datos para el rango de meses especificado.");
        }

        var groupedData = datos
            .GroupBy(d => d.codigo_parametro)
            .Select(g => new DeviceData
            {
                CodigoParametro = g.Key.ToString(),
                NombreParametro = g.FirstOrDefault()?.nombre_parametro ?? "",
                UnidadParametro = g.FirstOrDefault()?.unidad ?? "",
                AbreviacionParametro = g.FirstOrDefault()?.descripcion_corta?.Substring(0, 4) ?? "",
                Values = new DeviceValues
                {
                    AvgData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Average(d => d.valor_numero.Value) },
                    MinData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Min(d => d.valor_numero.Value) },
                    MaxData = new List<float> { (float)g.Where(d => d.valor_numero.HasValue).Max(d => d.valor_numero.Value) }
                }
            }).ToList();

        var response = new DeviceDataResponse
        {
            DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
            DeviceData = groupedData
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {

        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
});


app.Run();

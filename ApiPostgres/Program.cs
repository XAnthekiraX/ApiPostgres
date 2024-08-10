using ApiPostgres.Data;
using ApiPostgres.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<Sensors_db>(options =>
options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/sensores/{id}", async (int id, Sensors_db db) =>
{
    // Obtener datos por ID
    var datos = await db.datos_sensores
        .Where(d => d.id == id)
        .ToListAsync();

    if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para el ID especificado.");
    }

    // Agrupar datos por parámetro
    var groupedData = datos
        .GroupBy(d => d.codigo_parametro)
        .Select(g => new DeviceData
        {
            ParameterCode = g.Key.ToString(),
            ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
            ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
            ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
            Values = new Values
            {
                AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
            }
        }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});


app.MapGet("/sensores/porHora", async (string hora, Sensors_db db) =>
{
    // Intentar convertir la cadena de hora en un TimeOnly
    if (!TimeOnly.TryParse(hora, out TimeOnly horaOnly))
    {
        return Results.BadRequest("Formato de hora inválido. Utiliza un formato válido como 'HH:MM:SS'.");
    }

    // Convertir la hora a formato adecuado para la consulta SQL
    var horaFormatted = horaOnly.ToString("HH:mm:ss");

    // Consultar datos usando SQL crudo
    var datos = await db.datos_sensores
        .FromSqlRaw("SELECT *  FROM datos_sensores AS d WHERE d.fecha_dato::time='" + horaFormatted + "'")
        .ToListAsync();


    if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para la hora especificada.");
    }

    var groupedData = datos
        .GroupBy(d => d.codigo_parametro)
        .Select(g => new DeviceData
        {
            ParameterCode = g.Key.ToString(),
            ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
            ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
            ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
            Values = new Values
            {
                AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
            }
        }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});



app.MapGet("/sensores/porFecha", async (string fecha, Sensors_db db) =>
{
    // Intentar convertir la cadena de fecha en un DateTime
    if (!DateTime.TryParse(fecha, out DateTime fechaDateTime))
    {
        return Results.BadRequest("Formato de fecha inválido. Utiliza un formato válido como 'YYYY-MM-DD'.");
    }

    // Convertir la fecha a formato adecuado para la consulta SQL
    var fechaFormatted = fechaDateTime.ToString("yyyy-MM-dd");

    // Ejecutar la consulta con FromSqlRaw usando parámetros
    var datos = await db.datos_sensores
        .FromSqlRaw("SELECT * FROM datos_sensores WHERE date_trunc('day', fecha_dato) = @fechaFormatted::date",
            new NpgsqlParameter("@fechaFormatted", NpgsqlTypes.NpgsqlDbType.Date) { Value = fechaDateTime.Date })
        .ToListAsync();

    if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para la fecha especificada.");
    }

    var groupedData = datos
        .GroupBy(d => d.codigo_parametro)
        .Select(g => new DeviceData
        {
            ParameterCode = g.Key.ToString(),
            ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
            ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
            ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
            Values = new Values
            {
                AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
            }
        }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});



app.MapGet("/sensores/porRango", async (DateTime startDate, DateTime endDate, Sensors_db db) =>
{

    // Asegúrate de que las fechas se conviertan a UTC si es necesario
    var startDateUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
    var endDateUtc = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

    // Ejecutar la consulta con FromSqlRaw usando parámetros
    var datos = await db.datos_sensores
        .FromSqlRaw(
            "SELECT * FROM datos_sensores WHERE fecha_dato BETWEEN @startDate AND @endDate::date + INTERVAL '2 days'",
            new NpgsqlParameter("@startDate", NpgsqlTypes.NpgsqlDbType.TimestampTz) { Value = startDateUtc },
            new NpgsqlParameter("@endDate", NpgsqlTypes.NpgsqlDbType.TimestampTz) { Value = endDateUtc })
        .ToListAsync();

     if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para la fecha especificada.");
    }

    var groupedData = datos
         .GroupBy(d => d.codigo_parametro)
         .Select(g => new DeviceData
         {
             ParameterCode = g.Key.ToString(),
             ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
             ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
             ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
             Values = new Values
             {
                 AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                 MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                 MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
             }
         }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});


app.MapGet("/sensores/porSemana", async (DateTime fechaInicio, Sensors_db db) =>
{

    // Ejecutar la consulta con FromSqlRaw usando parámetros
    var datos = await db.datos_sensores
        .FromSqlRaw("WITH WeekRange AS (SELECT date_trunc('week', '"+fechaInicio+"'::date) AS week_start, date_trunc('week', '"+fechaInicio+"'::date) + INTERVAL '7 days' AS week_end) " +
        "SELECT * FROM datos_sensores, WeekRange WHERE fecha_dato >= week_start AND fecha_dato < week_end + INTERVAL '7 days'")
        .ToListAsync();

    if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para la fecha especificada.");
    }

    var groupedData = datos
        .GroupBy(d => d.codigo_parametro)
        .Select(g => new DeviceData
        {
            ParameterCode = g.Key.ToString(),
            ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
            ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
            ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
            Values = new Values
            {
                AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
            }
        }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});

app.MapGet("/sensores/porMes", async (int mes, Sensors_db db) =>
{

    // Ejecutar la consulta con FromSqlRaw usando parámetros
    var datos = await db.datos_sensores
        .FromSqlRaw("SELECT * FROM datos_sensores WHERE EXTRACT(MONTH FROM fecha_dato) = "+ mes +";")
        .ToListAsync();

    if (datos.Count == 0)
    {
        return Results.NotFound("No se encontraron datos para la fecha especificada.");
    }

    var groupedData = datos
         .GroupBy(d => d.codigo_parametro)
         .Select(g => new DeviceData
         {
             ParameterCode = g.Key.ToString(),
             ParameterName = g.FirstOrDefault()?.nombre_parametro ?? "",
             ParameterUnit = g.FirstOrDefault()?.valor_numero?.ToString() ?? "",
             ParameterAbbreviation = g.FirstOrDefault()?.nombre_parametro?.Substring(0, 4) ?? "", // Just an example
             Values = new Values
             {
                 AvgData = new List<double> { g.Average(d => (double)d.valor_numero) }, // Promedio en una lista
                 MinData = new List<double> { g.Min(d => (double)d.valor_numero) }, // Mínimo en una lista
                 MaxData = new List<double> { g.Max(d => (double)d.valor_numero) }  // Máximo en una lista
             }
         }).ToList();

    // Crear la respuesta final
    var response = new DeviceDataResponse
    {
        DeviceDates = datos.Select(d => d.fecha_dato.ToString("yyyy-MM-dd HH:mm:ss")).Distinct().ToList(),
        DeviceData = groupedData
    };

    return Results.Ok(response);
});



app.UseAuthorization();

app.MapControllers();

app.Run();

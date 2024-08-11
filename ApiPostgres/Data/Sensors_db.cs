using ApiPostgres.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPostgres.Data
{
    public class Sensors_db : DbContext
    {
        public Sensors_db(DbContextOptions<Sensors_db> options) : base(options)
        {
        }

        public DbSet<SensorDataResult> SensorDataResults { get; set; }
        public DbSet<Datos_Sensores> datos_sensores { get; set; }
        public DbSet<Parametros_Sensores> parametros_sensores { get; set; }
    }
}

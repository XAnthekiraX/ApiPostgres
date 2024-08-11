namespace ApiPostgres.Models
{
    public class SensorDataResult
    {
        public int id { get; set; }
        public int codigo_parametro { get; set; }
        public string? descripcion_corta { get; set; }
        public string? nombre_parametro { get; set; }
        public DateTime fecha_dato { get; set; }
        public float? valor_numero { get; set; }
        public string? unidad { get; set; }
    }
}

namespace ApiPostgres.Models
{
    public class Datos_Sensores
    {
        public int id { get; set; }  // Correspondiente a 'id'
        public int codigo_parametro { get; set; }  // Correspondiente a 'codigo_parametro'
        public int? parametro_sensores_id { get; set; }  // Correspondiente a 'parametro_sensores_id'
        public string? nombre_parametro { get; set; }  // Correspondiente a 'nombre_parametro'
        public DateTime fecha_dato { get; set; }  // Correspondiente a 'fecha_dato'
        public float? valor_numero { get; set; }  // Correspondiente a 'valor_numero'
    }
}

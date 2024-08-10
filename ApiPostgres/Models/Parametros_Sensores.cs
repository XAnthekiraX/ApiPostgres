namespace ApiPostgres.Models
{
    public class Parametros_Sensores
    {
        public int id { get; set; }  // Correspondiente a 'id'
        public int codigo_parametro { get; set; }  // Correspondiente a 'codigo_parametro'
        public string? descripcion_larga { get; set; }  // Correspondiente a 'descripcion_larga'
        public string? descripcion_med { get; set; }  // Correspondiente a 'descripcion_med'
        public string? descripcion_corta { get; set; }  // Correspondiente a 'descripcion_corta'
        public string? abreviacion { get; set; }  // Correspondiente a 'abreviacion'
        public string? observacion { get; set; }  // Correspondiente a 'observacion'
        public DateTime? fecha_creacion { get; set; }  // Correspondiente a 'fecha_creacion'
        public DateTime? fecha_modificacion { get; set; }  // Correspondiente a 'fecha_modificacion'
        public string? estado { get; set; }  // Correspondiente a 'estado'
        public string? unidad { get; set; }  // Correspondiente a 'unidad'
    }
}

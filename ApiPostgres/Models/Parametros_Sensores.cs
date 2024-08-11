namespace ApiPostgres.Models
{
    public class Parametros_Sensores
    {
        public int id { get; set; }  
        public int codigo_parametro { get; set; } 
        public string? descripcion_larga { get; set; } 
        public string? descripcion_med { get; set; }  
        public string? descripcion_corta { get; set; }  
        public string? abreviacion { get; set; }  
        public string? observacion { get; set; }  
        public DateTime? fecha_creacion { get; set; }  //  a 'fecha_creacion'
        public DateTime? fecha_modificacion { get; set; }  //  a 'fecha_modificacion'
        public string? estado { get; set; }  // Correspondiente a 'estado'
        public string? unidad { get; set; }  // Correspondiente a 'unidad'
    }
}

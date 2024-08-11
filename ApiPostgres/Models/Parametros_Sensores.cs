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
        public DateTime? fecha_creacion { get; set; }         
        public DateTime? fecha_modificacion { get; set; }         
        public string? estado { get; set; }          
        public string? unidad { get; set; }     
    }
}

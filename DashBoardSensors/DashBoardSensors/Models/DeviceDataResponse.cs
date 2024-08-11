// Archivo: DeviceDataResponse.cs

namespace DashBoardSensors.Models
{
  

    public class DeviceData
    {
        public string CodigoParametro { get; set; }  
        public string NombreParametro { get; set; }  
        public string UnidadParametro { get; set; }  
        public string AbreviacionParametro { get; set; }  
        public DeviceValues Values { get; set; }
    }

    public class DeviceDataResponse
    {
        public List<string> DeviceDates { get; set; } 
        public List<DeviceData> DeviceData { get; set; }  
    }
}

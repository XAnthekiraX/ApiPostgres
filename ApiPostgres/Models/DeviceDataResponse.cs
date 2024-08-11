using System.Collections.Generic;

namespace ApiPostgres.Models
{
    public class DeviceDataResponse
    {
        public List<string> DeviceDates { get; set; } = new List<string>();
        public List<DeviceData> DeviceData { get; set; } = new List<DeviceData>();
    }

    public class DeviceData
    {
        public string CodigoParametro { get; set; }
        public string NombreParametro { get; set; }
        public string UnidadParametro { get; set; }
        public string AbreviacionParametro { get; set; }
        public DeviceValues Values { get; set; }
    }

    public class DeviceValues
    {
        public List<float> AvgData { get; set; } = new List<float>();
        public List<float> MinData { get; set; } = new List<float>();
        public List<float> MaxData { get; set; } = new List<float>();
    }
}

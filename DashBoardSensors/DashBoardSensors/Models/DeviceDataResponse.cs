// Archivo: DeviceDataResponse.cs

namespace DashBoardSensors.Models
{
    public class DeviceValues
    {
        public List<double?> AvgData { get; set; }  // Cambiado a double?
        public List<double?> MinData { get; set; }  // Cambiado a double?
        public List<double?> MaxData { get; set; }  // Cambiado a double?
    }


    public class DeviceData
    {
        public string ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string ParameterUnit { get; set; }
        public string ParameterAbbreviation { get; set; }
        public DeviceValues Values { get; set; }
    }

    public class DeviceDataResponse
    {
        public List<string> DeviceDates { get; set; }
        public List<DeviceData> DeviceData { get; set; }
    }
}

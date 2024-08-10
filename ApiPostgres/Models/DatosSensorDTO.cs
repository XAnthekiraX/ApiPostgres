namespace ApiPostgres.Models
{
    public class DeviceDataResponse
    {
        public List<string> DeviceDates { get; set; } = new List<string>();
        public List<DeviceData> DeviceData { get; set; } = new List<DeviceData>();
    }

    public class DeviceData
    {
        public string ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string ParameterUnit { get; set; }
        public string ParameterAbbreviation { get; set; }
        public Values Values { get; set; }
    }

    public class Values
    {
        public List<double> AvgData { get; set; } = new List<double>();
        public List<double> MinData { get; set; } = new List<double>();
        public List<double> MaxData { get; set; } = new List<double>();
    }
}
// Archivo: SensorService.cs

using DashBoardSensors.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DashBoardSensors.Services
{
    public class SensorService : ISensorService
    {
        private readonly HttpClient _httpClient;

        public SensorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7038"); // Asegúrate de que la URL base sea correcta
        }

        public async Task<DeviceDataResponse> GetSensorDataAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/sensores/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var sensorData = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();
            return sensorData;
        }

        public async Task<DeviceDataResponse> GetSensorDataHourAsync(DateTime hora)
        {
            var horaFormatted = hora.ToString("HH:mm:ss"); // Asegúrate de que el formato coincida con el esperado en la API
            var response = await _httpClient.GetAsync($"/sensores/porHora?hora={horaFormatted}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var sensorData = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();
            return sensorData;
        }
        public async Task<DeviceDataResponse> GetSensorDataDateAsync(DateTime date)
        {
            var datteFormatted = date.ToString("yyyy-mm-dd"); // Asegúrate de que el formato coincida con el esperado en la API
            var response = await _httpClient.GetAsync($"/sensores/porfecha?fecha={datteFormatted}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var sensorData = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();
            return sensorData;
        }
    }
}

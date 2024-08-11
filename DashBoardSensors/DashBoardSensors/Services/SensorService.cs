using DashBoardSensors.Models;
using DashBoardSensors.Models;
using System;
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
            _httpClient.BaseAddress = new Uri("https://localhost:7038");
        }



        public async Task<DeviceDataResponse> GetDeviceDataHourRangeAsync(DateTime date, string hourStart, string hourEnd)
        {
            try
            {

                string formattedDate = date.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/sensores/porRangoHoras?fecha={formattedDate}&horaInicio={hourStart}&horaFin={hourEnd}");

                response.EnsureSuccessStatusCode();

                var deviceDataResponse = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();

                return deviceDataResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }

        public async Task<DeviceDataResponse> GetDeviceDataDateRangeAsync(DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                string formattedDateStart = dateStart.ToString("yyyy-MM-dd");
                string formattedDateEnd = dateEnd.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/sensores/porRangoFecha?startDate={formattedDateStart}&endDate={formattedDateEnd}");

                response.EnsureSuccessStatusCode();

                var deviceDataResponse = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();

                return deviceDataResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }

        public async Task<DeviceDataResponse> GetDeviceDataWeekRangeAsync(DateTime weekEnd)
        {
            try
            {
                string weekDateFormatt = weekEnd.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/sensores/porSemana?fechaInicio={weekDateFormatt}");

                response.EnsureSuccessStatusCode();

                var deviceDataResponse = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();

                return deviceDataResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }

        public async Task<DeviceDataResponse> GetDeviceDataMontRangeAsync(DateTime monthStart, DateTime monthEnd)
        {
            try
            {

                string monthDateFormattStart = monthStart.ToString("yyyy-MM-dd");
                string monthDateFormattEnd = monthEnd.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetAsync($"/sensores/porMes?fechaInicio={monthDateFormattStart}&fechaFin={monthDateFormattEnd}");

                response.EnsureSuccessStatusCode();

                var deviceDataResponse = await response.Content.ReadFromJsonAsync<DeviceDataResponse>();

                return deviceDataResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                throw;
            }
        }
    }
}

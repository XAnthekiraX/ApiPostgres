using DashBoardSensors.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DashBoardSensors.Services
{
    public class ServicioApi : IApiService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _baseUrl;

        public ServicioApi()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseUrl = builder.GetSection("ApiSettings").GetValue<string>("BaseUrl");

            // Configura el HttpClient una vez en el constructor
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Datos_Sensores_Api>> ObtenerTodosLosDatosSensores()
        {
            var response = await _client.GetAsync("/sensores");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Datos_Sensores_Api>>(jsonResponse);
                return data;
            }
            else
            {
                // Podrías manejar el error lanzando una excepción
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

    }
}

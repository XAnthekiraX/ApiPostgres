using DashBoardSensors.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DashBoardSensors.Services
{
    public class ApiService:IApiService
   {
        private string _baseUrl;

        public ApiService() { 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSetting:BaseUrl").Value;
        }

        public async Task<Datos_Sensores_Api> VerIdAsync(int id)
        {
            // Crear una instancia de HttpClient
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            // Realizar la solicitud GET al endpoint con el id
            var response = await client.GetAsync($"/sensores/{id}");

            // Verificar si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta como una cadena
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON en un objeto de tipo Datos_Sensores_Api
                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                // Retornar el objeto Datos_Sensores_Api
                return resultado;
            }
            else
            {
                // Si la respuesta no fue exitosa, puedes manejar el error o lanzar una excepción
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }


        public async Task<Datos_Sensores_Api> VerHoraAsync(string hora)
        {
          
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync($"/sensores/porHora?hora={hora}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                return resultado;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<Datos_Sensores_Api> VerFechaAsync(string fecha)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync($"/sensores/porFecha?fecha={fecha}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                return resultado;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public async Task<Datos_Sensores_Api> VerRangoAsync(DateTime startDate, DateTime endDate)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            // Formatear las fechas en formato adecuado para la consulta
            var startDateFormatted = startDate.ToString("yyyy-MM-ddTHH:mm:ss");
            var endDateFormatted = endDate.ToString("yyyy-MM-ddTHH:mm:ss");

            // Crear la URL con los parámetros de consulta
            var requestUri = $"/sensores/porRango?startDate={startDateFormatted}&endDate={endDateFormatted}";

            // Realizar la solicitud GET
            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta como una cadena
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON en un objeto de tipo Datos_Sensores_Api
                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                // Retornar el objeto Datos_Sensores_Api
                return resultado;
            }
            else
            {
                // Si la respuesta no fue exitosa, manejar el error o lanzar una excepción
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }


        public async Task<Datos_Sensores_Api> VerSemanaAsync(DateTime fechaInicio)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync($"/sensores/porSemana?fechainicio={fechaInicio}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                return resultado;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }
        public async Task<Datos_Sensores_Api> VerMesAsync(int mes)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync($"/sensores/porMes?=mes{mes}");

            if (response.IsSuccessStatusCode)
            {
                var jsonRespuesta = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<Datos_Sensores_Api>(jsonRespuesta);

                return resultado;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }
    }
}

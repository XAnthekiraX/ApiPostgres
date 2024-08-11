// Archivo: HomeController.cs

using DashBoardSensors.Services; // Agrega esta línea si no está presente
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DashBoardSensors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISensorService _sensorService;

        public HomeController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int sensorId)
        {
            var sensorData = await _sensorService.GetSensorDataAsync(sensorId);
            return View(sensorData);
        }

        [HttpGet]
        public async Task<IActionResult> PorHoraIndex(string horaStr)
        {
            if (DateTime.TryParse(horaStr, out var hora))
            {
                var sensorData = await _sensorService.GetSensorDataHourAsync(hora);
                return View(sensorData);
            }

            ViewBag.ErrorMessage = "Formato de hora inválido. Usa un formato válido como 'yyyy-MM-ddTHH:mm'.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PorFechaIndex(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out var date))
            {
                var sensorData = await _sensorService.GetSensorDataHourAsync(date);
                return View(sensorData);
            }

            ViewBag.ErrorMessage = "Formato de hora inválido. Usa un formato válido como 'yyyy-MM-dd'.";
            return View();
        }
    }
}

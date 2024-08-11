using DashBoardSensors.Models;
using DashBoardSensors.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> Index(DateTime? date, string hourStart, string hourEnd, DateTime? dateStart, DateTime? dateEnd, DateTime? weekEnd, DateTime? monthStart, DateTime? monthEnd)
        {

            if (date.HasValue && date.Value > DateTime.Now)
            {
                ViewBag.ErrorMessage = "The date cannot be in the future.";
                return View();
            }

            if ((!string.IsNullOrEmpty(hourStart) && !TimeSpan.TryParse(hourStart, out _)) ||
   (!string.IsNullOrEmpty(hourEnd) && !TimeSpan.TryParse(hourEnd, out _)))
            {
                ViewBag.ErrorMessage = "Please enter valid start and end hours.";
                return View();
            }

            if (dateStart.HasValue && dateEnd.HasValue)
            {
                if (dateStart.Value > dateEnd.Value)
                {
                    ViewBag.ErrorMessage = "The start date cannot be after the end date.";
                    return View();
                }

                if (dateEnd.Value > DateTime.Now)
                {
                    ViewBag.ErrorMessage = "The end date cannot be in the future.";
                    return View();
                }
            }

            if (weekEnd.HasValue && weekEnd.Value > DateTime.Now)
            {
                ViewBag.ErrorMessage = "The week end date cannot be in the future.";
                return View();
            }

            if (monthStart.HasValue && monthEnd.HasValue)
            {
                if (monthStart.Value > monthEnd.Value)
                {
                    ViewBag.ErrorMessage = "The start month cannot be after the end month.";
                    return View();
                }

                if (monthEnd.Value > DateTime.Now)
                {
                    ViewBag.ErrorMessage = "The end month cannot be in the future.";
                    return View();
                }
            }

            try
            {
                DeviceDataResponse deviceDataResponse;

                if (date.HasValue && !string.IsNullOrEmpty(hourStart) && !string.IsNullOrEmpty(hourEnd))
                {
                    deviceDataResponse = await _sensorService.GetDeviceDataHourRangeAsync(date.Value, hourStart, hourEnd);
                }
                else if (dateStart.HasValue && dateEnd.HasValue)
                {
                    deviceDataResponse = await _sensorService.GetDeviceDataDateRangeAsync(dateStart.Value, dateEnd.Value);
                }
                else if (weekEnd.HasValue)
                {
                    deviceDataResponse = await _sensorService.GetDeviceDataWeekRangeAsync(weekEnd.Value);
                }
                else if (monthStart.HasValue && monthEnd.HasValue)
                {
                    deviceDataResponse = await _sensorService.GetDeviceDataMontRangeAsync(monthStart.Value, monthEnd.Value);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter a sensor ID, a valid date and hour range, a date range, a week end date, or a month range.";
                    return View();
                }

                return View(deviceDataResponse);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while fetching data: {ex.Message}";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

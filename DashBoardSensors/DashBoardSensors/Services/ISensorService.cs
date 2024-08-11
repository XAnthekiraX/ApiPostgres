// Archivo: ISensorService.cs

using DashBoardSensors.Models;
using System.Threading.Tasks;

namespace DashBoardSensors.Services
{
    public interface ISensorService
    {
        Task<DeviceDataResponse> GetSensorDataAsync(int id);
        Task<DeviceDataResponse> GetSensorDataHourAsync(DateTime hora);
        Task<DeviceDataResponse> GetSensorDataDateAsync(DateTime date);
    }
}

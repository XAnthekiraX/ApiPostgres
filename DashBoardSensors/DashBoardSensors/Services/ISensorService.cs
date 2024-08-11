using DashBoardSensors.Models;
using System.Threading.Tasks;

namespace DashBoardSensors.Services
{
    public interface ISensorService
    {
        Task<DeviceDataResponse> GetDeviceDataHourRangeAsync(DateTime date, string hourStart, string hourEnd);
        Task<DeviceDataResponse> GetDeviceDataDateRangeAsync(DateTime dateStart, DateTime dateEnd);
        Task<DeviceDataResponse> GetDeviceDataWeekRangeAsync(DateTime weekEnd);
        Task<DeviceDataResponse> GetDeviceDataMontRangeAsync(DateTime monthStart, DateTime monthEnd);
    }
}

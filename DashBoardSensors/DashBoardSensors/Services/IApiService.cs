using DashBoardSensors.Models;

namespace DashBoardSensors.Services
{
    public interface IApiService
    {
        Task<Datos_Sensores_Api> VerIdAsync(int id);
        Task<Datos_Sensores_Api> VerHoraAsync(string hora);
        Task<Datos_Sensores_Api> VerFechaAsync(string fecha);
        Task<Datos_Sensores_Api> VerRangoAsync(DateTime startDate, DateTime endDate);
        Task<Datos_Sensores_Api> VerSemanaAsync(DateTime fechaInicio);
        Task<Datos_Sensores_Api> VerMesAsync(int mes);
    }
}

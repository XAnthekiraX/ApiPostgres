using DashBoardSensors.Models;
namespace DashBoardSensors.Services
{
    public interface IApiService
    {
        Task<Datos_Sensores_Api> VerId(int id);
        Task<Datos_Sensores_Api> VerHora(string hora);
        Task<Datos_Sensores_Api> VerFecha (DateTime fecha);
        Task<Datos_Sensores_Api> VerFechaRango(DateTime fechaRango1 , DateTime fechaRango2);
        Task<Datos_Sensores_Api> VerSemana(DateTime fechaSemana);
        Task<Datos_Sensores_Api> VerMes(DateTime fechaMes);
    }
}

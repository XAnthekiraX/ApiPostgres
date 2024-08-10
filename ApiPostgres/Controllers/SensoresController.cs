namespace ApiPostgres.Controllers
{
    using ApiPostgres.Data;
    using ApiPostgres.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class SensoresController : ControllerBase
    {
        private readonly Sensors_db _context;

        public SensoresController(Sensors_db context)
        {
            _context = context;
        }

    }
}

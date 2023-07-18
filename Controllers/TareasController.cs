using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareasServices _tareasServices;
        private readonly ILogger<TareasController> _logger;

        public TareasController(ITareasServices tareasServices, ILogger<TareasController> logger)
        {
            _tareasServices = tareasServices;
            _logger = logger;
        }

        [HttpPost("crearTarea")]
        public async Task<IActionResult> CrearTarea(AgregarTareasDTO model)
        {
            var result = await _tareasServices.CrearTarea(model);
            return Ok(result);
        }

        [HttpPut("cambiarEstadoTarea")]
        public async Task<IActionResult> CambiarEstadoTarea(CambiarEstadoDTO model)
        {
            var result = await _tareasServices.CambiarEstadoTarea(model);
            return Ok(result);
        }

        [HttpPut("cambiarPrioridadTarea")]
        public async Task<IActionResult> CambiarPrioridadTarea(CambiarPrioridadDTO model)
        {
            var result = await _tareasServices.CambiarPrioridadTarea(model);
            return Ok(result);
        }
    }
}

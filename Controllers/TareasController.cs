using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareasController : ControllerBase
    {
        private readonly ITareasServices _tareasServices;
        private readonly ILogger<TareasController> _logger;

        public TareasController(ITareasServices tareasServices, ILogger<TareasController> logger)
        {
            _tareasServices = tareasServices;
            _logger = logger;
        }

        [HttpGet("obtenerTareasByProyect/{IdProyecto}")]
        public async Task<IActionResult> ObtenerTareasByProyect(int IdProyecto)
        {
            var result = await _tareasServices.ObtenerTareasProyecto(IdProyecto);
            return Ok(result);
        }

        [HttpGet("obtenerTareasById/{IdProyecto}/{IdTarea}")]
        public async Task<IActionResult> ObtenerTareasById(int IdProyecto, int IdTarea)
        {
            var result = await _tareasServices.ObtenerTareasId_SP(IdProyecto, IdTarea);
            return Ok(result);
        }

        [HttpGet("obtenerTareaById/{IdProyecto}/{IdTarea}")]
        public async Task<IActionResult> ObtenerTareaById(int IdProyecto, int IdTarea)
        {
            var result = await _tareasServices.ObtenerTareaById(IdProyecto,IdTarea);
            return Ok(result);
        }

        [HttpGet("obtenerEncargadoTarea/{IdTarea}")]
        public async Task<IActionResult> ObtenerEncargadoTarea(int IdTarea)
        {
            var result = await _tareasServices.ObtenerEncargadoTarea(IdTarea);
            return Ok(result);
        }

        [HttpGet("tareasProximasEntregar/{IdUsuario}")]
        public async Task<IActionResult> TareasProximasEntregar(int IdUsuario)
        {
            var result = await _tareasServices.TareasProximasEntregar(IdUsuario);
            return Ok(result);
        }

        [HttpGet("tareasFinalizadas/{IdUsuario}")]
        public async Task<IActionResult> TareasFinalizadas(int IdUsuario)
        {
            var result = await _tareasServices.TareasFinalizadas(IdUsuario);
            return Ok(result);
        }

        [HttpGet("tareasRetrasadas/{IdUsuario}")]
        public async Task<IActionResult> TareasRetrasadas(int IdUsuario)
        {
            var result = await _tareasServices.TareasRetrasadas(IdUsuario);
            return Ok(result);
        }

        [HttpGet("tareasPorEstados/{IdProyecto}")]
        public async Task<IActionResult> MostrarTareasPorEstado(int IdProyecto)
        {
            var result = await _tareasServices.MostrarTareasPorEstado(IdProyecto);
            return Ok(result);
        }

        [HttpGet("tareasPorUsuario/{IdUsuario}")]
        public async Task<IActionResult> MostrarTareasPorUsuario(int IdUsuario)
        {
            var result = await _tareasServices.ListarTareasPorUsuario(IdUsuario);
            return Ok(result);
        }

        [HttpGet("descripcionTarea/{IdTarea}")]
        public async Task<IActionResult> MostrarDescripcionTarea(int IdTarea)
        {
            var result = await _tareasServices.MostrarDescripcionTarea(IdTarea);
            return Ok(result);
        }

        [HttpPost("agregarSubTareas/{IdTarea}")]
        public async Task<IActionResult> AgregarSubTareas(int IdTarea, List<AgregarTareasDTO> subTareas)
        {
            var result = await _tareasServices.AgregarSubTareas(IdTarea, subTareas);
            return Ok(result);
        }

        [HttpPost("crearTarea")]
        public async Task<IActionResult> CrearTarea(AgregarTareasDTO model)
        {
            var result = await _tareasServices.CrearTarea(model);
            return Ok(result);
        }

        [HttpPost("validarTarea/{IdTarea}")]
        public async Task<IActionResult> ValidarTarea(int IdTarea)
        {
            var result = await _tareasServices.TieneTareasPendiente(IdTarea);
            return Ok(result);
        }

        [HttpPost("guardarArchivoTarea")]
        public async Task<IActionResult> GuardarArchivoTarea([FromForm] IFormFile file)
        {
            var result = await _tareasServices.GuardarArchivosTareas(file);
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

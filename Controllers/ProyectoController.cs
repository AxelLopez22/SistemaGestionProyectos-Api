using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoServices _proyectoServices;
        private readonly ILogger<ProyectoController> _logger;

        public ProyectoController(IProyectoServices proyectoServices, ILogger<ProyectoController> logger)
        {
            _proyectoServices = proyectoServices;
            _logger = logger;
        }

        [HttpPost("crearProyecto")]
        public async Task<IActionResult> CreateProyect(CreateProyectoDTO model)
        {
            var result = await _proyectoServices.CreateProyect(model);
            return Ok(result);
        }

        [HttpGet("getUserByProyect/{IdProyect}")]
        public async Task<IActionResult> GetUserByProyect(int IdProyect)
        {
            var result = await _proyectoServices.GetUsersProyect(IdProyect);
            return Ok(result);
        }

        [HttpGet("getLeaderProyect/{IdProyect}")]
        public async Task<IActionResult> GetLeaderProyect(int IdProyect)
        {
            var result = await _proyectoServices.GetLeaderProyect(IdProyect);
            return Ok(result);
        }

        [HttpGet("getAllProyects")]
        public async Task<IActionResult> GetAllProyects()
        {
            var result = await _proyectoServices.GetAllProyects();
            return Ok(result);
        }

        [HttpGet("getProyectsByUser/{IdUsuario}")]
        public async Task<IActionResult> GetAllProyectsByUser(int IdUsuario)
        {
            var result = await _proyectoServices.GetProyectByUser(IdUsuario);
            return Ok(result);
        }

        [HttpGet("getProyectById/{IdProyect}")]
        public async Task<IActionResult> GetProyectById(int IdProyect)
        {
            var result = await _proyectoServices.GetProyectById(IdProyect);
            return Ok(result);
        }

        [HttpPut("updateDateProyect/{IdProyecto}/{NewDate}")]
        public async Task<IActionResult> UpdateDateProject(int IdProyecto, DateTime NewDate)
        {
            var result = await _proyectoServices.UpdateDateProject(IdProyecto, NewDate);
            return Ok(result);
        }

        [HttpPut("updateProyect/{IdProyecto}")]
        public async Task<IActionResult> UpdateProject(int IdProyecto, UpdateProyectoDto model)
        {
            var result = await _proyectoServices.UpdateProject(IdProyecto, model);
            return Ok(result);
        }

        [HttpPut("updateStateProyect/{IdProyecto}/{IdEstado}")]
        public async Task<IActionResult> UpdateStateProyect(int IdProyecto, int IdEstado)
        {
            var result = await _proyectoServices.UpdateStateProject(IdProyecto, IdEstado);
            return Ok(result);
        }
    }
}

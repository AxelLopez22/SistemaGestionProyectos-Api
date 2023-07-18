using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoUsuariosController : ControllerBase
    {
        private readonly ILogger<ProyectoUsuariosController> _logger;
        private readonly IProyectoUsuariosServices _userProjectServices;

        public ProyectoUsuariosController(ILogger<ProyectoUsuariosController> logger, IProyectoUsuariosServices userProjectServices)
        {
            _logger = logger;
            _userProjectServices = userProjectServices;
        }

        [HttpPost("agregarUsuarios")]
        public async Task<IActionResult> AsignarUsuarios(ProyectoUsuariosDTO model)
        {
            var result = await _userProjectServices.AgregarUsuarioAProyecto(model);
            return Ok(result);
        }
    }
}

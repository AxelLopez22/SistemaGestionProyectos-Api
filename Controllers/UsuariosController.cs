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
    public class UsuariosController : ControllerBase
    {
        private readonly IUserServices _services;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUserServices services, ILogger<UsuariosController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet("obtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var result = await _services.GetUsuarios();
            return Ok(result);
        }

        [HttpGet("ListarRoles")]
        public async Task<IActionResult> ListarRoles()
        {
            var result = await _services.GetRoles();
            return Ok(result);
        }

        [HttpPost("asignarRol")]
        public async Task<IActionResult> AsignarRol(AsignarRolDTO model)
        {
            var result = await _services.AsignarUserRole(model);
            return Ok(result);
        }
    }
}

using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}

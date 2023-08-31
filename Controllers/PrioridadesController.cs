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
    public class PrioridadesController : ControllerBase
    {
        private readonly IPrioridadServices _services;

        public PrioridadesController(IPrioridadServices services)
        {
            _services = services;
        }

        [HttpGet("listarPrioridades")]
        public async Task<IActionResult> ListarPrioridades()
        {
            var result = await _services.ListarPrioridades();
            return Ok(result);
        }
    }
}

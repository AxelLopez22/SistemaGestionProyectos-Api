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
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoServices _stateService;
        private readonly ILogger<EstadoController> _logger;

        public EstadoController(IEstadoServices stateService, ILogger<EstadoController> logger)
        {
            _stateService = stateService;
            _logger = logger;
        }

        [HttpGet("getState")]
        public async Task<IActionResult> GetStates()
        {
            var result = await _stateService.GetStates();
            return Ok(result);
        }

        [HttpGet("getStateByProyect/{IdProyect}")]
        public async Task<IActionResult> GetStateByProyect(int IdProyect)
        {
            var result = await _stateService.GetStatesByProyect(IdProyect);
            return Ok(result);
        }

        [HttpGet("getStateByTask/{IdTask}")]
        public async Task<IActionResult> GetStateByTask(int IdTask)
        {
            var result = await _stateService.GetStatesByTask(IdTask);
            return Ok(result);
        }
    }
}

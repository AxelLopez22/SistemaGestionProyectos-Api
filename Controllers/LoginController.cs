using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserServices _userServices;

        public LoginController(ILogger<LoginController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _userServices.Login(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromForm] CrearUsuarioDTO model)
        {
            var result = await _userServices.CreateUser(model);
            return Ok(result);
        }
    }
}

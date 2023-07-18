using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ILogger<ComentariosController> _logger;
        private readonly IComentariosServices _comentarioServices;

        public ComentariosController(ILogger<ComentariosController> logger, IComentariosServices comentarioServices)
        {
            _logger = logger;
            _comentarioServices = comentarioServices;
        }

        [HttpPost("agregarComentario")]
        public async Task<IActionResult> CrearComentario(ComentariosDTO model)
        {
            var result = await _comentarioServices.AgregarComentario(model);
            return Ok(result);
        }

        [HttpGet("{IdTarea}")]
        public async Task<IActionResult> ListarComentarios(int IdTarea)
        {
            var result = await _comentarioServices.ListarComentarios(IdTarea);
            return Ok(result);
        }
    }
}

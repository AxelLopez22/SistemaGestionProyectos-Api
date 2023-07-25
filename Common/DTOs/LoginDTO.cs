using Api_ProjectManagement.Common.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CrearUsuarioDTO
    {
        [Required]
        public string Nombres { get; set; } = null!;
        [Required]
        public string Apellidos { get; set; } = null!;
        [Required]
        public string Correo { get; set; } = null!;
        [Required]
        public string Contrasenia { get; set; } = null!;
        [PesoArchivoValidacion(PesoMaximo:10)]
        public IFormFile? Foto { get; set; }
    }

    public class GetUsuariosDTO
    {
        public int IdUsuario { get; set; }
        public string NombresCompleto { get; set;}
        public string Foto { get; set; }
    }

    public class GetLeaderProyectDTO
    {
        public int IdUsuario { get; set; }
        public string NombresCompleto { get; set; }
    }
}

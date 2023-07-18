using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.DTOs
{
    public class ProyectoUsuariosDTO
    {
        [Required]
        public int IdProyecto { get; set; }
        [Required]
        public List<int>? IdUsuario { get; set; }
    }
}

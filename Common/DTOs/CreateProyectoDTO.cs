using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.DTOs
{
    public class CreateProyectoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime FechaFin { get; set; }
        [Required]
        public int IdUsuario { get; set; }
    }

    public class GetAllProyect
    {
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string LiderProyecto { get; set; }
    }

    public class UpdateProyectoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ListProyectByUserDTO
    {
        public int IdProyecto { get; set;}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
    }
}

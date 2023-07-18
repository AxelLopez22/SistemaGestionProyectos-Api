using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.DTOs
{
    public class AgregarTareasDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime FechaFin { get; set; }
        [Required]
        public int IdProyecto { get; set; }
        public int IdTareaPadre { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        public int IdArchivo { get; set; }
        public int IdPrioridad { get; set; }
    }

    public class CambiarPrioridadDTO
    {
        public int IdTarea { get; set; }
        public int IdPrioridad { get; set; }
    }

    public class CambiarEstadoDTO
    {
        public int IdTarea { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstado { get; set; }
    }
}

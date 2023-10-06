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
        public int IdUsuarioCreador { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        public int? IdArchivo { get; set; }
        public int IdPrioridad { get; set; }
        public List<AgregarTareasDTO> SubTareas { get; set; }

        public AgregarTareasDTO()
        {
            this.SubTareas = new List<AgregarTareasDTO>();
        }
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

    public class TareaDTO
    {
        public int IdTarea { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
        public string Responsable { get; set; }
        public string UrlArchivo { get; set; }
        public string Prioridad { get; set; }
        public List<TareaDTO> SubTareas { get; set; }
    }

    public class sp_ListarTareas
    {
        [Key]
        public int IdTarea { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaFin { get; set; }
        public string Prioridad { get; set; }
    }

    public class sp_TareasEstados
    {
        public int IdTarea { get; set; }
        public string? Tarea { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string? Prioridad { get; set; }
        public string? Encargado { get; set; }
        public string? Foto { get; set; }
        public int IdEstado { get; set; }
    }

    public class sp_ListarTareasPorUsuario
    {
        public int IdTarea { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public string? Prioridad { get; set; }
        public DateTime FechaFin { get; set; }
    }
}

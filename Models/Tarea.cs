using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            Comentarios = new HashSet<Comentario>();
            HistorialTareas = new HashSet<HistorialTarea>();
            InverseIdTareaPadreNavigation = new HashSet<Tarea>();
        }

        public int IdTarea { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdEstado { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdTareaPadre { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdArchivo { get; set; }
        public int? IdPrioridad { get; set; }

        public virtual Archivo? IdArchivoNavigation { get; set; }
        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Prioridad? IdPrioridadNavigation { get; set; }
        public virtual Proyecto? IdProyectoNavigation { get; set; }
        public virtual Tarea? IdTareaPadreNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<HistorialTarea> HistorialTareas { get; set; }
        public virtual ICollection<Tarea> InverseIdTareaPadreNavigation { get; set; }
    }
}

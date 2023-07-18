using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            ProyectoUsuarios = new HashSet<ProyectoUsuario>();
            Tareas = new HashSet<Tarea>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdEstado { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Estado? IdEstadoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<ProyectoUsuario> ProyectoUsuarios { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Tareas = new HashSet<Tarea>();
        }

        public int IdPrioridad { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}

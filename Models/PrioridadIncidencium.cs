using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class PrioridadIncidencium
    {
        public PrioridadIncidencium()
        {
            Incidencia = new HashSet<Incidencia>();
        }

        public int IdPrioridad { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}

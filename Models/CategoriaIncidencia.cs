using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class CategoriaIncidencia
    {
        public CategoriaIncidencia()
        {
            Incidencia = new HashSet<Incidencia>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}

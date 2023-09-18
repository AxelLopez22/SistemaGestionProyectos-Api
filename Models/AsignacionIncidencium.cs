using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class AsignacionIncidencium
    {
        public int IdAsignacion { get; set; }
        public bool? Confirmada { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdIncidencia { get; set; }

        public virtual Incidencia? IdIncidenciaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

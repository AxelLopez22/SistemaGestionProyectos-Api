using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class ProyectoUsuario
    {
        public int IdProyectoUsuarios { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProyecto { get; set; }
        public bool? Estado { get; set; }

        public virtual Proyecto? IdProyectoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

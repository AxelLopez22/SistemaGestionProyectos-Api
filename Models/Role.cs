using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            UsuariosRoles = new HashSet<UsuariosRole>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<UsuariosRole> UsuariosRoles { get; set; }
    }
}

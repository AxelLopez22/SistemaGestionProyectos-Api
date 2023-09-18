using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Sistema
    {
        public Sistema()
        {
            Incidencia = new HashSet<Incidencia>();
            UsuariosSistemas = new HashSet<UsuariosSistema>();
            VersionSistemas = new HashSet<VersionSistema>();
        }

        public int IdSistema { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<Incidencia> Incidencia { get; set; }
        public virtual ICollection<UsuariosSistema> UsuariosSistemas { get; set; }
        public virtual ICollection<VersionSistema> VersionSistemas { get; set; }
    }
}

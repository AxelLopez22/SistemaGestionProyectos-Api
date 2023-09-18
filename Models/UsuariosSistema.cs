using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class UsuariosSistema
    {
        public int IdUsuarioSistema { get; set; }
        public bool? Estado { get; set; }
        public int? IdSistema { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Sistema? IdSistemaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

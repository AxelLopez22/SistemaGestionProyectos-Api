using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class VersionSistema
    {
        public int IdVersion { get; set; }
        public string? VersionProyecto { get; set; }
        public string? Descripcion { get; set; }
        public bool? Obsoleto { get; set; }
        public bool? Desarrollo { get; set; }
        public bool? Estable { get; set; }
        public int? IdSistema { get; set; }

        public virtual Sistema? IdSistemaNavigation { get; set; }
    }
}

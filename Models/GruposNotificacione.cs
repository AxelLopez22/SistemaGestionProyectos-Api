using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class GruposNotificacione
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProyecto { get; set; }
        public string? IdConexion { get; set; }
    }
}

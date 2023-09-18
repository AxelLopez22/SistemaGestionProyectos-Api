using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Nota
    {
        public int IdNota { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? Estado { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdIncidencia { get; set; }

        public virtual Incidencia? IdIncidenciaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

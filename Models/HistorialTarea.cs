using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class HistorialTarea
    {
        public int IdHistorialTarea { get; set; }
        public int? IdTarea { get; set; }
        public int? IdEstadoNuevo { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Tarea? IdTareaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool? Estado { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdTarea { get; set; }
        public int? IdArchivo { get; set; }

        public virtual Archivo? IdArchivoNavigation { get; set; }
        public virtual Tarea? IdTareaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

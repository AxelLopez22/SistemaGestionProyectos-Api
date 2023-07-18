using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            Comentarios = new HashSet<Comentario>();
            Tareas = new HashSet<Tarea>();
        }

        public int IdArchivo { get; set; }
        public string Nombre { get; set; } = null!;
        public string UrlArchivo { get; set; } = null!;

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}

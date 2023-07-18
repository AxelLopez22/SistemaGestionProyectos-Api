using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comentarios = new HashSet<Comentario>();
            HistorialTareas = new HashSet<HistorialTarea>();
            ProyectoUsuarios = new HashSet<ProyectoUsuario>();
            Proyectos = new HashSet<Proyecto>();
            Tareas = new HashSet<Tarea>();
            UsuariosRoles = new HashSet<UsuariosRole>();
        }

        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public string? Foto { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<HistorialTarea> HistorialTareas { get; set; }
        public virtual ICollection<ProyectoUsuario> ProyectoUsuarios { get; set; }
        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<UsuariosRole> UsuariosRoles { get; set; }
    }
}

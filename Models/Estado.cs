﻿using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Proyectos = new HashSet<Proyecto>();
            Tareas = new HashSet<Tarea>();
        }

        public int IdEstado { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}

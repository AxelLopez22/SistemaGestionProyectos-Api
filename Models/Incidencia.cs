using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class Incidencia
    {
        public Incidencia()
        {
            AsignacionIncidencia = new HashSet<AsignacionIncidencium>();
            HistorialIncidencia = new HashSet<HistorialIncidencia>();
            Nota = new HashSet<Nota>();
        }

        public int IdIncidencia { get; set; }
        public string? Resumen { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdReproducibilidad { get; set; }
        public int? IdSeveridad { get; set; }
        public int? IdPrioridad { get; set; }
        public int? IdStatus { get; set; }
        public int? IdSistema { get; set; }

        public virtual CategoriaIncidencia? IdCategoriaNavigation { get; set; }
        public virtual PrioridadIncidencium? IdPrioridadNavigation { get; set; }
        public virtual Reproducibilidad? IdReproducibilidadNavigation { get; set; }
        public virtual Severidad? IdSeveridadNavigation { get; set; }
        public virtual Sistema? IdSistemaNavigation { get; set; }
        public virtual StatusIncidencia? IdStatusNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<AsignacionIncidencium> AsignacionIncidencia { get; set; }
        public virtual ICollection<HistorialIncidencia> HistorialIncidencia { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
    }
}

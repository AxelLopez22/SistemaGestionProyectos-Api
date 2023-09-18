using System;
using System.Collections.Generic;

namespace Api_ProjectManagement.Models
{
    public partial class HistorialIncidencia
    {
        public int Id { get; set; }
        public string? NombreCampo { get; set; }
        public string? AntiguoValor { get; set; }
        public string? NuevoValor { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdIncidencia { get; set; }

        public virtual Incidencia? IdIncidenciaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}

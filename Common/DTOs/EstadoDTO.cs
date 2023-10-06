namespace Api_ProjectManagement.Common.DTOs
{
    public class EstadoDTO
    {
        public int IdEstado { get; set; } 
        public string Nombre { get; set; }
    }

    public class HistorialEstadoDTO
    {
        public string? Usuario { get; set; }
        public string? Estados { get; set; }
        public DateTime Fecha { get; set; }
    }
}

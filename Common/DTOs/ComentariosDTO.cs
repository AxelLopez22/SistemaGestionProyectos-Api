using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.DTOs
{
    public class ComentariosDTO
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdTarea { get; set; }
        public int IdArchivo { get; set; }
    }

    public class ListarComentariosDTO
    {
        public int IdComentario { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }
}

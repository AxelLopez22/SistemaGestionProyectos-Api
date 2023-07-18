using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class ComentariosServices : IComentariosServices
    {
        private readonly ProjectManagementDBContext _context;

        public ComentariosServices(ProjectManagementDBContext context)
        {
            _context = context;
        }

        public async Task<ModelResponse> AgregarComentario(ComentariosDTO model)
        {
            var response = new ModelResponse();

            Comentario comentario = new Comentario();
            comentario.Descripcion = model.Descripcion;
            comentario.IdComentario = model.IdUsuario;
            comentario.IdArchivo = model.IdArchivo;
            comentario.IdTarea = model.IdTarea;

            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ModelResponse> ListarComentarios(int IdTarea)
        {
            var response = new ModelResponse();

            var result = await _context.Comentarios.Where(x => x.Estado == true && x.IdTarea == IdTarea)
                .Select(s => new ListarComentariosDTO()
                {
                     IdComentario = s.IdComentario,
                     Descripcion = s.Descripcion,
                     Usuario = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos
                }).ToListAsync();

            if(result.Count() != 0)
            {
                response.Success = true;
                response.Data = result;
                response.Message = MensajeReferencia.ConsultaExitosa;

                return response;
            }

            response.Success = false;
            response.Data = null;
            response.Message = MensajeReferencia.RecursoNoEncontrado;
            return response;
        }
    }

    public interface IComentariosServices
    {
        Task<ModelResponse> AgregarComentario(ComentariosDTO model);
        Task<ModelResponse> ListarComentarios(int IdTarea);
    }
}

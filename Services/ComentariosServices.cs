using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Api_ProjectManagement.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class ComentariosServices : IComentariosServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IHubContext<HubCommentNotify> _hubContext;


        public ComentariosServices(ProjectManagementDBContext context, IHubContext<HubCommentNotify> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
            //_hubContext = hubContext;
        }

        public async Task<ModelResponse> AgregarComentario(ComentariosDTO model)
        {
            var response = new ModelResponse();

            try
            {
                Comentario comentario = new Comentario();
                comentario.Descripcion = model.Descripcion;
                comentario.IdUsuario = model.IdUsuario;
                comentario.IdArchivo = model.IdArchivo;
                comentario.IdTarea = model.IdTarea;
                comentario.Fecha = DateTime.Now;

                await _context.Comentarios.AddAsync(comentario);
                await _context.SaveChangesAsync();

                var nuevoComentario = await _context.Comentarios.Where(x => x.IdComentario == comentario.IdComentario)
                    .Select(s => new ListarComentariosDTO()
                    {
                        IdComentario = s.IdComentario,
                        Descripcion = s.Descripcion,
                        Usuario = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos,
                        Foto = s.IdUsuarioNavigation.Foto,
                        Fecha = (DateTime)s.Fecha,
                        IdTarea = (int)s.IdTarea
                    }).FirstAsync();

                //int idProyecto = (int)await _context.Tareas.Where(x => x.IdTarea == model.IdTarea).Select(x => x.IdProyecto).FirstAsync();

                await _hubContext.Clients.All.SendAsync("NotifyComment", nuevoComentario);

                response.Success = true;
                response.Data = nuevoComentario;
                response.Message = MensajeReferencia.AgregarComentario;

                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Data = ex;
                response.Message = ex.Message; 
                return response;
            }
           
        }

        public async Task<ModelResponse> ListarComentarios(int IdTarea)
        {
            var response = new ModelResponse();
            try
            {
                var result = await _context.Comentarios.Where(x => x.Estado == true && x.IdTarea == IdTarea)
                    .Select(s => new ListarComentariosDTO()
                    {
                        IdComentario = s.IdComentario,
                        Descripcion = s.Descripcion,
                        Usuario = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos,
                        Foto = s.IdUsuarioNavigation.Foto,
                        Fecha = (DateTime)s.Fecha,
                        IdTarea = (int)s.IdTarea
                    }).ToListAsync();

                if (result.Count() != 0)
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
            catch(Exception ex)
            {
                response.Success = false;
                response.Data = ex;
                response.Message = ex.Message;
                return response;
            }
            
        }
    }

    public interface IComentariosServices
    {
        Task<ModelResponse> AgregarComentario(ComentariosDTO model);
        Task<ModelResponse> ListarComentarios(int IdTarea);
    }
}

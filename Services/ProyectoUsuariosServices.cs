using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class ProyectoUsuariosServices : IProyectoUsuariosServices
    {
        private ProjectManagementDBContext _context;

        public ProyectoUsuariosServices(ProjectManagementDBContext context)
        {
            _context = context;
        }

        public async Task<ModelResponse> AgregarUsuarioAProyecto(ProyectoUsuariosDTO model)
        {
            var response = new ModelResponse();

            foreach(var item in model.IdUsuario)
            {
                ProyectoUsuario userProject = new ProyectoUsuario();
                userProject.IdProyecto = model.IdProyecto;
                userProject.IdUsuario = item;

                await _context.ProyectoUsuarios.AddAsync(userProject);
                await _context.SaveChangesAsync();
            }
            
            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.IntegrantesAgregados;

            return response;
        }

        public async Task<ModelResponse> EliminarUsuarioProyect(int IdProyecto, int IdUsuario)
        {
            var response = new ModelResponse();

            var result = await _context.ProyectoUsuarios.Where(x => x.IdProyecto == IdProyecto && x.IdUsuario == IdUsuario && x.Estado == true)
                .FirstOrDefaultAsync();

            result.Estado = false;
            _context.ProyectoUsuarios.Update(result);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.UpdateSuccess;

            return response;
        }
    }

    public interface IProyectoUsuariosServices
    {
        Task<ModelResponse> AgregarUsuarioAProyecto(ProyectoUsuariosDTO model);
        Task<ModelResponse> EliminarUsuarioProyect(int IdProyecto, int IdUsuario);
    }
}

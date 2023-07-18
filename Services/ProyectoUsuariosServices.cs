using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;

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
    }

    public interface IProyectoUsuariosServices
    {
        Task<ModelResponse> AgregarUsuarioAProyecto(ProyectoUsuariosDTO model);
    }
}

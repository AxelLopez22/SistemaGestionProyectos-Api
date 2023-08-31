using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class EstadoServices : IEstadoServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;

        public EstadoServices(ProjectManagementDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelResponse> GetStates()
        {
            var response = new ModelResponse();

            var result = await _context.Estados
                .Select(s => new EstadoDTO()
                {
                    IdEstado = s.IdEstado,
                    Nombre = s.Nombre
                }).ToListAsync();

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetStatesByProyect(int IdProyect)
        {
            var response = new ModelResponse();

            var proyecto = await _context.Proyectos.Where(x => x.IdProyecto == IdProyect).FirstOrDefaultAsync();

            var estado = await _context.Estados
                .Where(x => x.IdEstado == (proyecto.IdProyecto == null ? 0 : proyecto.IdEstado))
                .Select(x => new EstadoDTO()
                {
                    IdEstado = x.IdEstado,
                    Nombre = x.Nombre
                })
                .FirstOrDefaultAsync();

            response.Success = true;
            response.Data = estado;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetStatesByTask(int IdTask)
        {
            ModelResponse response = new ModelResponse();

            //var task = await _context.Tareas.Where(x => x.IdTarea == IdTask).FirstOrDefaultAsync();


            var state = await _context.Tareas.Where(x => x.IdTarea == IdTask)
                .Select(x => new EstadoDTO()
                {
                    IdEstado = x.IdEstadoNavigation.IdEstado,
                    Nombre = x.IdEstadoNavigation.Nombre
                }).FirstOrDefaultAsync();

            response.Success = true;
            response.Data = state;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }
    }

    public interface IEstadoServices
    {
        Task<ModelResponse> GetStates();
        Task<ModelResponse> GetStatesByProyect(int IdProyect);
        Task<ModelResponse> GetStatesByTask(int IdTask);
    }
}

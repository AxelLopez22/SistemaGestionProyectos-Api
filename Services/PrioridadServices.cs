using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class PrioridadServices : IPrioridadServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;

        public PrioridadServices(ProjectManagementDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelResponse> ListarPrioridades()
        {
            var response = new ModelResponse();

            var prioridades = await _context.Prioridads
                .Select(s => new PrioridadDTO()
                {
                    IdPrioridad = s.IdPrioridad,
                    Nombre = s.Nombre
                }).ToListAsync();

            //var result = _mapper.Map<PrioridadDTO>(prioridades);

            response.Success = true;
            response.Data = prioridades;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }
    }

    public interface IPrioridadServices
    {
        Task<ModelResponse> ListarPrioridades();
    }
}

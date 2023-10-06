using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class EstadoServices : IEstadoServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public EstadoServices(ProjectManagementDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
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

        public async Task<ModelResponse> GetStatesOfTask(int IdTask)
        {
            var response = new ModelResponse();
            List<HistorialEstadoDTO> estados = new List<HistorialEstadoDTO>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ConnectionDb")))
            {
                await connection.OpenAsync();

                string Query = "sp_EstadoTareas";

                SqlCommand command = new SqlCommand(Query, connection);
                command.Parameters.AddWithValue("@IdTarea", IdTask);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    estados.Add(new HistorialEstadoDTO()
                    {
                        Usuario = reader["Usuario"].ToString(),
                        Estados = reader["Estados"].ToString(),
                        Fecha = Convert.ToDateTime(reader["Fecha"])
                    });
                }

                await reader.CloseAsync();
                await connection.CloseAsync();

                response.Success = true;
                response.Data = estados;
                response.Message = MensajeReferencia.ConsultaExitosa;

                return response;
            }
        }
    }

    public interface IEstadoServices
    {
        Task<ModelResponse> GetStates();
        Task<ModelResponse> GetStatesByProyect(int IdProyect);
        Task<ModelResponse> GetStatesByTask(int IdTask);
        Task<ModelResponse> GetStatesOfTask(int IdTask);
    }
}

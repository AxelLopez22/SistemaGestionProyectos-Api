using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class TareasServices : ITareasServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;

        public TareasServices(ProjectManagementDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelResponse> CambiarEstadoTarea(CambiarEstadoDTO model)
        {
            var response = new ModelResponse();
            HistorialTarea historial = new HistorialTarea();

            var tarea = await _context.Tareas.Where(x => x.IdTarea == model.IdTarea).FirstOrDefaultAsync();

            if(tarea == null)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.TareaNoEncontrada;

                return response;
            }

            historial.IdTarea = model.IdTarea;
            historial.IdEstadoNuevo = model.IdEstado;
            historial.FechaCambio = DateTime.Now;
            historial.IdUsuario = model.IdUsuario;

            tarea.IdEstado = model.IdEstado;

            _context.Tareas.Update(tarea);
            await _context.HistorialTareas.AddAsync(historial);

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.ActualizarTarea;

            return response;
        }

        public async Task<ModelResponse> CambiarPrioridadTarea(CambiarPrioridadDTO model)
        {
            var response = new ModelResponse();

            var tarea = await _context.Tareas.Where(x => x.IdTarea == model.IdTarea).FirstOrDefaultAsync();

            if (tarea == null)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.TareaNoEncontrada;

                return response;
            }

            tarea.IdPrioridad = model.IdPrioridad;
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.ActualizarTarea;

            return response;
        }

        public async Task<ModelResponse> CrearTarea(AgregarTareasDTO model)
        {
            var response = new ModelResponse();
            var tarea = _mapper.Map<Tarea>(model);

            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.CrearTarea;

            return response;
        }
    }

    public interface ITareasServices
    {
        Task<ModelResponse> CrearTarea(AgregarTareasDTO model);
        Task<ModelResponse> CambiarPrioridadTarea(CambiarPrioridadDTO model);
        Task<ModelResponse> CambiarEstadoTarea(CambiarEstadoDTO model);
    }
}

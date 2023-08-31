﻿using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api_ProjectManagement.Services
{
    public class TareasServices : ITareasServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;
        private readonly IFilesServices _filesServices;
        //private readonly IHubContext<HubServices> _hubContext;

        public TareasServices(ProjectManagementDBContext context, IMapper mapper, IFilesServices filesServices)
        {
            _context = context;
            _mapper = mapper;
            _filesServices = filesServices;
            //_hubContext = hubContext;
        }

        public async Task<ModelResponse> AgregarSubTareas(int IdTarea, List<AgregarTareasDTO> subTareas)
        {
            ModelResponse response = new ModelResponse();
            try
            {
                foreach (var item in subTareas)
                {
                    Tarea tarea = new Tarea();
                    tarea.Nombre = item.Nombre;
                    tarea.Descripcion = item.Descripcion;
                    tarea.FechaInicio = DateTime.Now;
                    tarea.FechaFin = item.FechaFin;
                    tarea.IdProyecto = item.IdProyecto;
                    tarea.IdPrioridad = item.IdPrioridad;
                    tarea.IdUsuario = item.IdUsuario;
                    tarea.IdEstado = 1;
                    tarea.IdArchivo = item.IdArchivo;
                    tarea.IdTareaPadre = IdTarea;

                    await _context.Tareas.AddAsync(tarea);
                    await _context.SaveChangesAsync();
                }

                response.Success = true;
                response.Data = null;
                response.Message = MensajeReferencia.CrearTarea;

                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Data = ex.Data;
                response.Message = ex.Message;

                return response;
            }
            
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
            try
            {
                Tarea task = new Tarea();
                task.Nombre = model.Nombre;
                task.Descripcion = model.Descripcion;
                task.FechaInicio = DateTime.Now;
                task.FechaFin = model.FechaFin;
                task.IdProyecto = model.IdProyecto;
                task.IdPrioridad = model.IdPrioridad;
                task.IdUsuario = model.IdUsuario;
                task.IdEstado = 1;
                task.IdArchivo = model.IdArchivo;

                await _context.Tareas.AddAsync(task);
                await _context.SaveChangesAsync();

                foreach (var item in model.SubTareas)
                {
                    Tarea Subtask = new Tarea();
                    Subtask.Nombre = item.Nombre;
                    Subtask.Descripcion = item.Descripcion;
                    Subtask.FechaInicio = DateTime.Now;
                    Subtask.FechaFin = item.FechaFin;
                    Subtask.IdProyecto = item.IdProyecto;
                    Subtask.IdPrioridad = item.IdPrioridad;
                    Subtask.IdTareaPadre = task.IdTarea;
                    Subtask.IdEstado = 1;
                    Subtask.IdUsuario = item.IdUsuario;
                    Subtask.IdArchivo = item.IdArchivo;

                    await _context.Tareas.AddAsync(Subtask);
                }

                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = null;
                response.Message = MensajeReferencia.CrearTarea;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex;
                response.Message = ex.Message;
                return response;
            }
            
        }

        public async Task<ModelResponse> GuardarArchivosTareas(IFormFile data)
        {
            ModelResponse response = new ModelResponse();
            string contenedor = "Archivos";
            string UrlBD = "";

            if (data != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await data.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(data.FileName);
                    UrlBD = await _filesServices.GuardarArchivo(contenido, extension, contenedor, data.ContentType);

                }
            }

            if(UrlBD != "")
            {
                Archivo archivo = new Archivo();
                archivo.Nombre = data.FileName;
                archivo.UrlArchivo = UrlBD;
                
                await _context.Archivos.AddAsync(archivo);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = archivo;
                response.Message = MensajeReferencia.ConsultaExitosa;

                return response;
            }

            response.Success = false;
            response.Data = null;
            response.Message = MensajeReferencia.ErrorServidor;

            return response;
        }

        public async Task<ModelResponse> ObtenerEncargadoTarea(int IdTarea)
        {
            ModelResponse response = new ModelResponse();

            var result = await _context.Tareas.Where(x => x.IdTarea == IdTarea)
                .Select(s => new GetLeaderProyectDTO()
                {
                    IdUsuario = s.IdUsuarioNavigation.IdUsuario,
                    NombresCompleto = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos
                }).FirstOrDefaultAsync();

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> ObtenerTareaById(int IdProyecto, int IdTarea)
        {
            ModelResponse res = new ModelResponse();

            var tasks = await _context.Tareas.Where(x => x.IdProyecto == IdProyecto && x.IdTareaPadre == null && x.IdTarea == IdTarea)
                    .Select(s => new TareaDTO()
                    {
                        IdTarea = s.IdTarea,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        FechaInicio = s.FechaFin,
                        FechaFin = s.FechaFin,
                        Estado = s.IdEstadoNavigation.Nombre,
                        Prioridad = s.IdPrioridadNavigation.Nombre,
                        Responsable = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos,
                        UrlArchivo = s.IdArchivoNavigation.UrlArchivo,
                        SubTareas = null
                    })
                .ToListAsync();

            if (tasks == null)
            {
                res.Success = false;
                res.Data = null;
                res.Message = MensajeReferencia.ErrorServidor;

                return res;
            }

            res.Success = true;
            res.Data = tasks;
            res.Message = MensajeReferencia.ConsultaExitosa;

            return res;
        }

        public async Task<ModelResponse> ObtenerTareasId_SP(int IdProyecto, int IdTarea)
        {
            ModelResponse res = new ModelResponse();

            var tasks = await _context.Tareas.Where(x => x.IdProyecto == IdProyecto && x.IdTareaPadre == null && x.IdTarea == IdTarea)
                    .Select(s => new TareaDTO()
                    {
                        IdTarea = s.IdTarea,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        FechaInicio = s.FechaFin,
                        FechaFin = s.FechaFin,
                        Estado = s.IdEstadoNavigation.Nombre,
                        Prioridad = s.IdPrioridadNavigation.Nombre,
                        Responsable = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos,
                        UrlArchivo = s.IdArchivoNavigation.UrlArchivo,
                        SubTareas = _context.Tareas.Where(x => x.IdTareaPadre == s.IdTarea)
                            .Select(st => new TareaDTO()
                            {
                                IdTarea = st.IdTarea,
                                Nombre = st.Nombre,
                                Descripcion = st.Descripcion,
                                FechaInicio = st.FechaInicio,
                                FechaFin = st.FechaFin,
                                Estado = st.IdEstadoNavigation.Nombre,
                                Prioridad = st.IdPrioridadNavigation.Nombre,
                                Responsable = st.IdUsuarioNavigation.Nombres + " " + st.IdUsuarioNavigation.Apellidos,
                                UrlArchivo = st.IdArchivoNavigation.UrlArchivo
                            }).ToList()
                    })
                .ToListAsync();

            if (tasks == null)
            {
                res.Success = false;
                res.Data = null;
                res.Message = MensajeReferencia.ErrorServidor;

                return res;
            }

            res.Success = true;
            res.Data = tasks;
            res.Message = MensajeReferencia.ConsultaExitosa;

            return res;
        }

        public async Task<ModelResponse> ObtenerTareasProyecto(int IdProyecto)
        {
            var response = new ModelResponse();

            try
            {
                var tasks = await _context.Tareas.Where(x => x.IdProyecto == IdProyecto && x.IdTareaPadre == null)
                    .Select(s => new TareaDTO()
                    {
                        IdTarea = s.IdTarea,
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion,
                        FechaInicio = s.FechaFin,
                        FechaFin = s.FechaFin,
                        Estado = s.IdEstadoNavigation.Nombre,
                        Prioridad = s.IdPrioridadNavigation.Nombre,
                        Responsable = s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos,
                        UrlArchivo = s.IdArchivoNavigation.UrlArchivo,
                        SubTareas = _context.Tareas.Where(x => x.IdTareaPadre == s.IdTarea)
                            .Select(st => new TareaDTO()
                            {
                                IdTarea = st.IdTarea,
                                Nombre = st.Nombre,
                                Descripcion = st.Descripcion,
                                FechaInicio = st.FechaInicio,
                                FechaFin = st.FechaFin,
                                Estado = st.IdEstadoNavigation.Nombre,
                                Prioridad = st.IdPrioridadNavigation.Nombre,
                                Responsable = st.IdUsuarioNavigation.Nombres + " " + st.IdUsuarioNavigation.Apellidos,
                                UrlArchivo = st.IdArchivoNavigation.UrlArchivo
                            }).ToList()
                    })
                .ToListAsync();

                response.Success = true;
                response.Data = tasks;
                response.Message = MensajeReferencia.ConsultaExitosa;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = ex;
                response.Message = ex.Message;
                return response;
            }
            
        }

        public async Task<ModelResponse> TareasFinalizadas(int IdUsuario)
        {
            ModelResponse response = new ModelResponse();

            var result = await _context.Sp_ListarTareas.FromSqlInterpolated($"EXEC sp_TareasFinalizadas {IdUsuario}").ToListAsync();

            if(result.Count() == 0)
            {
                response.Success = false;
                response.Data = "No hay tareas para mostrar";
                response.Message = MensajeReferencia.TareaNoEncontrada;
                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> TareasProximasEntregar(int IdUsuario)
        {
            ModelResponse response = new ModelResponse();

            var result = await _context.Sp_ListarTareas.FromSqlInterpolated($"EXEC sp_TareasProximasEntregar {IdUsuario}").ToListAsync();

            if (result.Count() == 0)
            {
                response.Success = false;
                response.Data = "No hay tareas para mostrar";
                response.Message = MensajeReferencia.TareaNoEncontrada;
                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> TareasRetrasadas(int IdUsuario)
        {
            ModelResponse response = new ModelResponse();

            var result = await _context.Sp_ListarTareas.FromSqlInterpolated($"EXEC sp_TareasRetrasadas {IdUsuario}").ToListAsync();

            if (result.Count() == 0)
            {
                response.Success = false;
                response.Data = "No hay tareas para mostrar";
                response.Message = MensajeReferencia.TareaNoEncontrada;
                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }
    }

    public interface ITareasServices
    {
        Task<ModelResponse> CrearTarea(AgregarTareasDTO model);
        Task<ModelResponse> CambiarPrioridadTarea(CambiarPrioridadDTO model);
        Task<ModelResponse> CambiarEstadoTarea(CambiarEstadoDTO model);
        Task<ModelResponse> ObtenerTareasProyecto(int IdProyecto);
        Task<ModelResponse> GuardarArchivosTareas(IFormFile data);
        Task<ModelResponse> ObtenerTareasId_SP(int IdProyecto, int IdTarea);
        Task<ModelResponse> ObtenerTareaById(int IdProyecto, int IdTarea);
        Task<ModelResponse> ObtenerEncargadoTarea(int IdTarea);
        Task<ModelResponse> TareasProximasEntregar(int IdUsuario);
        Task<ModelResponse> TareasRetrasadas(int IdUsuario);
        Task<ModelResponse> TareasFinalizadas(int IdUsuario);
        Task<ModelResponse> AgregarSubTareas(int IdTarea, List<AgregarTareasDTO> subTareas);
    }
}

﻿using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_ProjectManagement.Services
{
    public class ProyectoServices : IProyectoServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly IMapper _mapper;

        public ProyectoServices(ProjectManagementDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelResponse> CreateProyect(CreateProyectoDTO model)
        {
            var response = new ModelResponse();
            Proyecto proyecto = new Proyecto();
            ProyectoUsuario userPro = new ProyectoUsuario();

            proyecto = _mapper.Map<Proyecto>(model);


            await _context.Proyectos.AddAsync(proyecto);
            await _context.SaveChangesAsync();

            userPro.IdProyecto = proyecto.IdProyecto;
            userPro.IdUsuario = model.IdUsuario;
            userPro.Estado = true;

            await _context.ProyectoUsuarios.AddAsync(userPro);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.ProyectoCreado;

            return response;
        }

        public async Task<ModelResponse> GetAllProyects()
        {
            var response = new ModelResponse();

            var result = await _context.Proyectos.Select(s => new GetAllProyect()
            {
                IdProyecto = s.IdProyecto,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                FechaInicio = s.FechaInicio,
                FechaFin = (DateTime)s.FechaFin,
                LiderProyecto = (s.IdUsuarioNavigation.Nombres + " " + s.IdUsuarioNavigation.Apellidos)
            }).ToListAsync();

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetLeaderProyect(int IdProyecto)
        {
            var response = new ModelResponse();

            var result = await _context.Proyectos.Where(x => x.IdProyecto == IdProyecto)
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

        public async Task<ModelResponse> GetProyectById(int IdProyecto)
        {
            var response = new ModelResponse();

            var result = await _context.Proyectos.Where(x => x.IdProyecto == IdProyecto)
                .Select(x => new ListProyectByUserDTO()
                {
                    IdProyecto = x.IdProyecto,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Estado = x.IdEstadoNavigation.Nombre
                }).ToListAsync();

            if (result.Count() == 0)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.RecursoNoEncontrado;

                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetProyectByUser(int IdUsuario)
        {
            var response = new ModelResponse();

            var result = await _context.ProyectoUsuarios
                .Where(x => x.IdUsuario == IdUsuario)
                .Select(x => new ListProyectByUserDTO()
                {
                    IdProyecto = x.IdProyectoNavigation.IdProyecto,
                    Nombre = x.IdProyectoNavigation.Nombre,
                    Descripcion = x.IdProyectoNavigation.Descripcion,
                    Estado = x.IdProyectoNavigation.IdEstadoNavigation.Nombre
                }).ToListAsync();

            if(result.Count() == 0)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.RecursoNoEncontrado;

                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetUsersProyect(int IdProyecto)
        {
            var response = new ModelResponse();

            var result = await _context.ProyectoUsuarios
                .Where(x => x.IdProyecto == IdProyecto && x.Estado == true)
                .Select(s => new GetUsuariosDTO()
                {
                    IdUsuario = (int)s.IdUsuarioNavigation.IdUsuario,
                    NombresCompleto = s.IdUsuarioNavigation.Nombres + ' ' + s.IdUsuarioNavigation.Apellidos,
                    Foto = s.IdUsuarioNavigation.Foto
                }).ToListAsync();

            if(result.Count == 0)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.ErrorServidor;

                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<bool> UpdateDateProject(int IdProyecto, DateTime NewDate)
        {
            var response = new ModelResponse();
            var project = await _context.Proyectos.Where(x => x.IdProyecto == IdProyecto).FirstOrDefaultAsync();

            if(project != null)
            {
                project.FechaFin = NewDate;

                _context.Proyectos.Update(project);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateProject(int IdProyecto, UpdateProyectoDto model)
        {
            var response = new ModelResponse();
            var project = await _context.Proyectos.Where(x => x.IdProyecto == IdProyecto).FirstOrDefaultAsync();

            if (project != null)
            {
                project.Nombre = model.Nombre;
                project.Descripcion = model.Descripcion;

                _context.Proyectos.Update(project);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateStateProject(int IdProyecto, int IdEstado)
        {
            var response = new ModelResponse();
            var project = await _context.Proyectos.Where(x => x.IdProyecto == IdProyecto).FirstOrDefaultAsync();

            if (project != null)
            {
                project.IdEstado = IdEstado;

                _context.Proyectos.Update(project);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }

    public interface IProyectoServices
    {
        Task<ModelResponse> CreateProyect(CreateProyectoDTO model);
        Task<ModelResponse> GetAllProyects();
        Task<bool> UpdateDateProject(int IdProyecto, DateTime NewDate);
        Task<bool> UpdateProject(int IdProyecto, UpdateProyectoDto model);
        Task<bool> UpdateStateProject(int IdProyecto, int IdEstado);
        Task<ModelResponse> GetProyectByUser(int IdUsuario);
        Task<ModelResponse> GetProyectById(int IdProyecto);
        Task<ModelResponse> GetUsersProyect(int IdProyecto);
        Task<ModelResponse> GetLeaderProyect(int IdProyecto);
    }
}

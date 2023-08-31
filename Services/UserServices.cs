using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Common.Exceptions;
using Api_ProjectManagement.Common.References;
using Api_ProjectManagement.Common.Util;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api_ProjectManagement.Services
{
    public class UserServices : IUserServices
    {
        private readonly ProjectManagementDBContext _context;
        private readonly ITokenServices _tokenServices;
        private readonly IFilesServices _fileServices;

        public UserServices(ProjectManagementDBContext context, ITokenServices tokenServices, IFilesServices fileServices)
        {
            _context = context;
            _tokenServices = tokenServices;
            _fileServices = fileServices;
        }

        public async Task<ModelResponse> AsignarUserRole(AsignarRolDTO model)
        {
            ModelResponse response = new ModelResponse();
            
            UsuariosRole userRole = new UsuariosRole();
            userRole.IdUsuario = model.IdUsuario;
            userRole.IdRol = model.IdRol;

            await _context.UsuariosRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Data = null;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> CreateUser(CrearUsuarioDTO model)
        {
            try
            {
                var response = new ModelResponse();

                string contenedor = "Fotos";
                string passwordHash = Encrypt.GetSHA256(model.Contrasenia);
                Usuario user = new Usuario();
                user.Nombres = model.Nombres;
                user.Apellidos = model.Apellidos;
                user.Correo = model.Correo;
                user.Contrasenia = passwordHash;

                if (model.Foto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(model.Foto.FileName);
                        user.Foto = await _fileServices.GuardarArchivo(contenido, extension, contenedor, model.Foto.ContentType);
                    }
                }

                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = null;
                response.Message = MensajeReferencia.UserCreated;
                return response;
            } catch
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, MensajeReferencia.FailedCreatedUser);
            }
        }

        public async Task<ModelResponse> GetRoles()
        {
            ModelResponse response = new ModelResponse();

            var result = await _context.Roles.Where(x => x.Estado == true)
                .Select(s => new ListarRoles()
                {
                    IdRol = s.IdRol,
                    Nombre = s.Nombre,
                }).ToListAsync();

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> GetUsuarios()
        {
            var response = new ModelResponse();
            var result = await _context.Usuarios.Where(x => x.Estado == true)
                .Select(s => new GetUsuariosDTO()
                {
                    IdUsuario = s.IdUsuario,
                    NombresCompleto = s.Nombres + ' ' + s.Apellidos,
                    Foto = s.Foto
                }).ToListAsync();

            response.Success = true;
            response.Data = result;
            response.Message = MensajeReferencia.ConsultaExitosa;

            return response;
        }

        public async Task<ModelResponse> Login(LoginDTO model)
        {
            var response = new ModelResponse();
            var hasPassword = Encrypt.GetSHA256(model.Password);

            var usuario = await _context.Usuarios.Where(x => x.Correo == model.Email && x.Contrasenia == hasPassword).FirstOrDefaultAsync();

            if(usuario == null)
            {
                response.Success = false;
                response.Data = null;
                response.Message = MensajeReferencia.ErrorCredenciales;
            }

            response.Success = true;
            response.Data = await _tokenServices.GenerateToken(model);
            response.Message = MensajeReferencia.LoginSuccess;

            return response;
        }
    }

    public interface IUserServices
    {
        Task<ModelResponse> Login(LoginDTO model);
        Task<ModelResponse> CreateUser(CrearUsuarioDTO model);
        Task<ModelResponse> GetUsuarios();
        Task<ModelResponse> GetRoles();
        Task<ModelResponse> AsignarUserRole(AsignarRolDTO model);
    }
}

using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Common.DTOs.Response;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_ProjectManagement.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly ProjectManagementDBContext _context;

        public TokenServices(IConfiguration config, ProjectManagementDBContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<AuthResponse> GenerateToken(LoginDTO login)
        {
            var Usuario = new Usuario();
            Usuario = await _context.Usuarios.Where(x => x.Correo == login.Email).FirstOrDefaultAsync();
            var roles = await _context.UsuariosRoles.Where(x => x.IdUsuario == Usuario.IdUsuario).Select(s => s.IdRolNavigation.Nombre).ToListAsync();

            var Claims = new List<Claim>()
            {
                new Claim("Nombre", Usuario.Nombres),
                new Claim("Apellidos", Usuario.Apellidos),
                new Claim("foto", Usuario.Foto),
                new Claim("IdUsuario", Usuario.IdUsuario.ToString())
            };

            Claims.AddRange(roles.Select(rol => new Claim("Rol", rol)));

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["LlaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var Expiracion = DateTime.UtcNow.AddHours(8);
            var securityToken = new JwtSecurityToken(claims: Claims,
                expires: Expiracion, signingCredentials: creds);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };
        }
    }

    public interface ITokenServices
    {
        Task<AuthResponse> GenerateToken(LoginDTO login);
    }
}

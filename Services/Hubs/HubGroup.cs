using Api_ProjectManagement.Common.DTOs;
using Api_ProjectManagement.Database;
using Api_ProjectManagement.Models;
using Microsoft.AspNetCore.SignalR;

namespace Api_ProjectManagement.Services.Hubs
{
    public class HubGroup : Hub
    {
        private readonly ProjectManagementDBContext _context;

        public HubGroup(ProjectManagementDBContext context)
        {
            _context = context;
        }

        public async Task AgregarAlGrupo(string projectId, string userId)
        {
            GruposNotificacione grupo = new GruposNotificacione();

            //grupo.IdProyecto = Int32.Parse(projectId);
            //grupo.IdUsuario = Int32.Parse(userId);
            //grupo.IdConexion = Context.ConnectionId;

            //await _context.GruposNotificaciones.AddAsync(grupo);
            //await _context.SaveChangesAsync();

            //await Clients.Client(Context.ConnectionId).SendAsync("ConextionId", Context.ConnectionId);
            
            //await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
            Console.WriteLine("Esto es una nueva conexion: " + Context.ConnectionId + "IdProyecto: " + projectId + "IdUsuario: " + userId);
        }

        public async Task RemoverDelGrupo(string UserId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserId);
        }

        public async Task CrearTarea(TareaDTO model)
        {
            await Clients.All.SendAsync("tareaCreada", model);
        }
    }
}

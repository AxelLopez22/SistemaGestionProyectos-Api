using Api_ProjectManagement.Common.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Api_ProjectManagement.Services.Hubs
{
    public class HubGroup : Hub
    {
        public async Task AgregarAlGrupo(string projectId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
            Console.WriteLine("Esto es una nueva conexion" + Context.ConnectionId + "IdProyecto" + projectId);
        }

        public async Task RemoverDelGrupo(string projectId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId);
        }

        public async Task CrearTarea(TareaDTO model)
        {
            await Clients.All.SendAsync("tareaCreada", model);
        }

        public async Task NotifyAddProyect(string idUser, string mensaje)
        {

        }
    }
}

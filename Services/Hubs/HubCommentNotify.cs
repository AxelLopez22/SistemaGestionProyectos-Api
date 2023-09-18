using Api_ProjectManagement.Common.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Api_ProjectManagement.Services.Hubs
{
    public class HubCommentNotify : Hub
    {
        public async Task NotifyComment(string proyect, ListarComentariosDTO model)
        {
            Console.WriteLine($"Se agregó un comentario: {model.Descripcion}");
            await Clients.Group(proyect).SendAsync("NuevoComentario", model);
        }
    }
}

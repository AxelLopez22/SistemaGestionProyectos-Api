using Api_ProjectManagement.Common.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Api_ProjectManagement.Services.Hubs
{
    public class HubCommentNotify : Hub
    {
        private readonly IHubContext<HubCommentNotify> _hubContext;

        public HubCommentNotify(IHubContext<HubCommentNotify> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyComment(string proyect, ListarComentariosDTO model)
        {
            Console.WriteLine($"Se agregó un comentario: {model.Descripcion}");
            await _hubContext.Clients.Group(proyect).SendAsync("NuevoComentario", model);
        }
    }
}

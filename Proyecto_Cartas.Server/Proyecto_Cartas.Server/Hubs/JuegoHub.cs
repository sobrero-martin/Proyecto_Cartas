using Microsoft.AspNetCore.SignalR;

namespace Proyecto_Cartas.Server.Hubs
{
    public class JuegoHub : Hub
    {
        public static Dictionary<int, List<string>> conexionesPorPartida = new();

        public async Task UnirsePartida(int partidaId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"partida-{partidaId}");
        }

        public async Task NotificarPartidaEncontrada(int partidaId, string mensaje)
        {
            await Clients.Group($"Partida-{partidaId}").SendAsync("RecibirNotificacionPartida", mensaje);
        }
    }
}

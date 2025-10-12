using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IEstadoCartaRepositorio : IRepositorio<EstadoCarta>
    {
        Task<EstadoCartaDTO?> CambiarPosicion(int id, string nuevaPosicion);
        Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId);
        Task<EstadoCartaDTO?> EnviarAlCementerio(int usuarioPartidaId, int cartaId);
        Task<List<EstadoCartaDTO>> EstadoCartaDeUnUsuario(int usuarioPartidaId);
        Task<List<EstadoCartaDTO>> FiltrarPosicion(int usuarioPartidaId, string posicion);
        Task<EstadoCartaDTO?> RobarCarta(int usuarioPartidaId);
        Task<List<EstadoCartaDTO>> RobarCartasCantidad(int usuarioPartidaId, int cantidad);
    }
}

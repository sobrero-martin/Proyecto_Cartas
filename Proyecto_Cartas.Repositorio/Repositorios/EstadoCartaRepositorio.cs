using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class EstadoCartaRepositorio : Repositorio<EstadoCarta>, IEstadoCartaRepositorio
    {
        private readonly AppDbContext context;

        public EstadoCartaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<EstadoCartaDTO>> EstadoCartaDeUnUsuario(int usuarioPartidaId)
        {
            var estado = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId).Select(ec => new EstadoCartaDTO
                {
                    Id = ec.Id,
                    UsuarioPartidaID = ec.UsuarioPartidaID,
                    InventarioID = ec.InventarioID,
                    Ataque = ec.Ataque,
                    Vida = ec.Vida,
                    Velocidad = ec.Velocidad,
                    Posicion = ec.Posicion
                }).ToListAsync();

            return estado;
        }

        public async Task<List<EstadoCartaDTO>> FiltrarPosicion(int usuarioPartidaId, string posicion)
        {
            var pos = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId && ec.Posicion == posicion).Select(ec => new EstadoCartaDTO
                {
                    Id = ec.Id,
                    UsuarioPartidaID = ec.UsuarioPartidaID,
                    InventarioID = ec.InventarioID,
                    Ataque = ec.Ataque,
                    Vida = ec.Vida,
                    Velocidad = ec.Velocidad,
                    Posicion = ec.Posicion
                }).ToListAsync(); ;
            return pos;
        }

        public async Task<EstadoCartaDTO?> CambiarPosicion(int id, string nuevaPosicion)
        {
            var estadoCarta = await context.EstadosCarta.FirstOrDefaultAsync(ec => ec.Id == id);
            if (estadoCarta == null)
            {
                return null;
            }
            estadoCarta.Posicion = nuevaPosicion;
            await context.SaveChangesAsync();

            return new EstadoCartaDTO
            {
                Id = estadoCarta.Id,
                UsuarioPartidaID = estadoCarta.UsuarioPartidaID,
                InventarioID = estadoCarta.InventarioID,
                Ataque = estadoCarta.Ataque,
                Vida = estadoCarta.Vida,
                Velocidad = estadoCarta.Velocidad,
                Posicion = estadoCarta.Posicion
            };
        }

        public async Task<EstadoCartaDTO?> RobarCarta(int usuarioPartidaId)
        {
            var cartasEnMazo = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId && ec.Posicion == "Mazo")
                .ToListAsync();
            if (cartasEnMazo.Count == 0)
            {
                return null; // No hay cartas en el mazo
            }

            var random = new Random();
            var cartaSeleccionada = cartasEnMazo[random.Next(cartasEnMazo.Count)];

            var cartaRobada = await CambiarPosicion(cartaSeleccionada.Id, "Mano");
            return cartaRobada;
        }

        public async Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId)
        {
            var cartaSeleccionada = await context.EstadosCarta
                .FirstOrDefaultAsync(ec =>
                    ec.UsuarioPartidaID == usuarioPartidaId &&
                    ec.Id == cartaId &&
                    ec.Posicion == "Mano");
            if (cartaSeleccionada == null)
            {
                return null; // La carta especificada no está en la mano
            }
            var cartaEnCampo = await CambiarPosicion(cartaSeleccionada.Id, "Campo");
            return cartaEnCampo;
        }

        public async Task<EstadoCartaDTO?> EnviarAlCementerio(int usuarioPartidaId, int cartaId)
        {
            var cartaSeleccionada = await context.EstadosCarta
                .FirstOrDefaultAsync(ec =>
                    ec.UsuarioPartidaID == usuarioPartidaId &&
                    ec.Id == cartaId &&
                    (ec.Posicion == "Mano" || ec.Posicion == "Campo"));
            if (cartaSeleccionada == null)
            {
                return null; // La carta especificada no está en la mano o en el campo
            }
            var cartaEnCementerio = await CambiarPosicion(cartaSeleccionada.Id, "Cementerio");
            return cartaEnCementerio;
        }

        public async Task<List<EstadoCartaDTO>> RobarCartasCantidad(int usuarioPartidaId, int cantidad)
        {
            var cartasEnMazo = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId && ec.Posicion == "Mazo")
                .ToListAsync();

            if (cartasEnMazo.Count == 0)
            {
                return new List<EstadoCartaDTO>(); // No hay cartas en el mazo
            }

            var random = new Random();
            var cartaSeleccionadas = cartasEnMazo.OrderBy(r => random.Next()).Take(cantidad).ToList();

            var cartasRobadas = new List<EstadoCartaDTO>();

            foreach (var carta in cartaSeleccionadas)
            {
                var cartaRobada = await CambiarPosicion(carta.Id, "Mano");
                if (cartaRobada != null)
                {
                    cartasRobadas.Add(cartaRobada);
                }
                
            }

            return cartasRobadas; 

        }


    }
}

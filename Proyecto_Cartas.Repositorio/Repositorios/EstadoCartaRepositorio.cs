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
        private readonly IEventoRepositorio eventoRepositorio;

        public EstadoCartaRepositorio(AppDbContext context, IEventoRepositorio eventoRepositorio) : base(context)
        {
            this.context = context;
            this.eventoRepositorio = eventoRepositorio;
        }


        public async Task<List<EstadoCartaDTO>> EstadoCartaDeUnUsuario(int usuarioPartidaId)
        {

            var estado = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId).Select(ec => new EstadoCartaDTO
                {
                    Id = ec.Id,
                    UsuarioPartidaID = ec.UsuarioPartidaID,
                    InventarioID = ec.InventarioID,
                    Nombre = ec.Nombre,
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
                    Nombre = ec.Nombre,
                    Ataque = ec.Ataque,
                    Vida = ec.Vida,
                    Velocidad = ec.Velocidad,
                    Posicion = ec.Posicion
                }).ToListAsync(); ;
            return pos;
        }

        public async Task<EstadoCartaDTO?> CambiarPosicion(int id, string nuevaPosicion, int turnoId, string accion)
        {
            var estadoCarta = await context.EstadosCarta.FirstOrDefaultAsync(ec => ec.Id == id);
            if (estadoCarta == null)
            {
                return null;
            }
            var originalPosicion = estadoCarta.Posicion;
            estadoCarta.Posicion = nuevaPosicion;
            await context.SaveChangesAsync();
            try
            {
                var evento = await eventoRepositorio.CrearEvento(new EventoCrearDTO
                {
                    TurnoID = turnoId,
                    EstadoCartaID = id,
                    Accion = accion,
                    Origen = originalPosicion,
                    Destino = nuevaPosicion
                });
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrar el error)
                Console.WriteLine($"Error al crear el evento: {ex.Message}");
            }

            return new EstadoCartaDTO
            {
                Id = estadoCarta.Id,
                UsuarioPartidaID = estadoCarta.UsuarioPartidaID,
                InventarioID = estadoCarta.InventarioID,
                Nombre = estadoCarta.Nombre,
                Ataque = estadoCarta.Ataque,
                Vida = estadoCarta.Vida,
                Velocidad = estadoCarta.Velocidad,
                Posicion = estadoCarta.Posicion
            };
        }

        public async Task<EstadoCartaDTO?> RobarCarta(int usuarioPartidaId, int turnoId)
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

            var cartaRobada = await CambiarPosicion(cartaSeleccionada.Id, "Mano", turnoId, "Robo");
            return cartaRobada;
        }

        public async Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId, int lugar, int turnoId)
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

            var campoOcupado = await context.EstadosCarta
                                     .FirstOrDefaultAsync(c => c.UsuarioPartidaID == usuarioPartidaId && c.Posicion == $"Campo{lugar}");
            if (campoOcupado != null) return null; // La posicion del campo ya esta ocupada

            var cartaEnCampo = await CambiarPosicion(cartaSeleccionada.Id, $"Campo{lugar}", turnoId, "Colocaren Campo");
            return cartaEnCampo;
        }

        public async Task<EstadoCartaDTO?> EnviarAlCementerio(int usuarioPartidaId, int cartaId, int turnoId)
        {
            var cartaSeleccionada = await context.EstadosCarta
                .FirstOrDefaultAsync(ec =>
                    ec.UsuarioPartidaID == usuarioPartidaId &&
                    ec.Id == cartaId &&
                    (ec.Posicion == "Mano" || ec.Posicion == "Campo1" || ec.Posicion == "Campo2" || ec.Posicion == "Campo3"));
            if (cartaSeleccionada == null)
            {
                return null; // La carta especificada no está en la mano o en el campo
            }
            var cartaEnCementerio = await CambiarPosicion(cartaSeleccionada.Id, "Cementerio", turnoId, "Enviar al Cementerio");
            return cartaEnCementerio;
        }

        public async Task<List<EstadoCartaDTO>> RobarCartasCantidad(int usuarioPartidaId, int cantidad, int turnoId)
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
                var cartaRobada = await CambiarPosicion(carta.Id, "Mano", turnoId, "Robo");
                if (cartaRobada != null)
                {
                    cartasRobadas.Add(cartaRobada);
                    //try
                    //{
                    //    var evento = await eventoRepositorio.CrearEvento(new EventoCrearDTO
                    //    {
                    //        TurnoID = turnoId,
                    //        EstadoCartaID = cartaRobada.Id,
                    //        Accion = "Robo",
                    //        Origen = "Mazo",
                    //        Destino = "Mano"
                    //    });
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Manejar la excepción (por ejemplo, registrar el error)
                    //    Console.WriteLine($"Error al crear el evento: {ex.Message}");
                    //}
                }

            }

            return cartasRobadas;

        }

        public async Task<List<EstadoCartaDTO>> CartasEnCampo(int usuarioPartidaId)
        {
            var pos = await context.EstadosCarta
                .Where(ec => ec.UsuarioPartidaID == usuarioPartidaId && (ec.Posicion == "Campo1" 
                                                                            || ec.Posicion == "Campo2"
                                                                            || ec.Posicion == "Campo3"))
                .Select(ec => new EstadoCartaDTO
                {
                    Id = ec.Id,
                    UsuarioPartidaID = ec.UsuarioPartidaID,
                    InventarioID = ec.InventarioID,
                    Nombre = ec.Nombre,
                    Ataque = ec.Ataque,
                    Vida = ec.Vida,
                    Velocidad = ec.Velocidad,
                    Posicion = ec.Posicion
                }).ToListAsync();
            return pos;
        }

    }
}

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

        public async Task<int> CrearEstadosCartas(int usuarioPartidaId, string tipoMazo)
        {
            var usuarioPartida = await context.UsuariosPartida
                .Include(up => up.PerfilUsuario)
                .FirstOrDefaultAsync(up => up.Id == usuarioPartidaId);

            if (usuarioPartida == null)
                return 0;

            bool yaTiene = await context.EstadosCarta
                .AnyAsync(ec => ec.UsuarioPartidaID == usuarioPartidaId);

            if (yaTiene)
                return 2;

            var cartasMazo = await context.Inventarios
                .Where(c => c.Tipo == tipoMazo && c.PerfilUsuarioID == usuarioPartida.PerfilUsuarioID)
                .Include(c => c.Carta)
                .ToListAsync();

            if (cartasMazo.Count == 0)
                return 0;

            foreach (var carta in cartasMazo)
            {
                var estadoCarta = new EstadoCarta
                {
                    UsuarioPartidaID = usuarioPartidaId,
                    InventarioID = carta.Id,
                    Nombre = carta.Carta!.NombreCarta,
                    Ataque = carta.Carta.Ataque,
                    Vida = carta.Carta.Vida,
                    Velocidad = carta.Carta.Velocidad,
                    Posicion = "Mazo"
                };
                context.EstadosCarta.Add(estadoCarta);
            }

            await context.SaveChangesAsync();

            return 1;
        }

        public async Task<List<EstadoCartaDTO>> ObtenerCartasEnCampo(int idPartida)
        {
            var posicionesCampo = new[] { "Campo1", "Campo2", "Campo3" };

            return await context.EstadosCarta
                .Where(c =>
                    c.UsuarioPartida!.Partida!.Id == idPartida &&
                    posicionesCampo.Contains(c.Posicion) &&
                    c.Vida > 0)
                .Select(c => new EstadoCartaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Ataque = c.Ataque,
                    Vida = c.Vida,
                    Velocidad = c.Velocidad,
                    Posicion = c.Posicion,
                    UsuarioPartidaID = c.UsuarioPartidaID
                })
                .OrderByDescending(c => c.Velocidad)
                .ToListAsync();
        }

        public async Task<List<EventoDTO>> Batalla(int idPartida, int turnoId)
        {
            var cartas = await ObtenerCartasEnCampo(idPartida);

            var prioridades = new Dictionary<string, string[]>
            {
                { "Campo1", new[] { "Campo1", "Campo2", "Campo3" } },
                { "Campo2", new[] { "Campo2", "Campo1", "Campo3" } },
                { "Campo3", new[] { "Campo3", "Campo2", "Campo1" } }
            };

            var eventos = new List<EventoDTO>();

            var turno = await context.Turnos
                .Include(t => t.UsuarioPartida)
                .FirstOrDefaultAsync(t => t.Id == turnoId);

            var yaHayEventos = await context.Eventos.AnyAsync(e => e.Turno.UsuarioPartida.PartidaID == idPartida && e.Turno.Numero == turno.Numero && e.Accion == "Ataque");

            if(yaHayEventos)
            {
                var eventosPrevios = await context.Eventos
                    .Where(e => e.TurnoID == turnoId && (e.Accion == "Ataque" || e.Accion == "Enviar al Cementerio"))
                    .Select(e => new EventoDTO { Origen = e.Origen, Destino = e.Destino })
                    .ToListAsync();
                return eventosPrevios;
            }

            foreach (var atacante in cartas)
            {
                if (atacante.Vida <= 0)
                    continue;

                EstadoCartaDTO? defensor = null;

                foreach (var pos in prioridades[atacante.Posicion])
                {
                    defensor = cartas
                        .FirstOrDefault(c =>
                        c.UsuarioPartidaID != atacante.UsuarioPartidaID &&
                        c.Posicion == pos &&
                        c.Vida > 0);

                    if (defensor != null)
                            break;
                }

                if (defensor != null)
                {
                    var evento = await Ataque(atacante, defensor, turnoId);
                    eventos.Add(evento);
                }
            }
           
            await context.SaveChangesAsync();
            return eventos;
        }

        public async Task<EventoDTO> Ataque(EstadoCartaDTO atacante, EstadoCartaDTO defensor, int turnoId)
        {
            defensor.Vida -= atacante.Ataque;
            if (defensor.Vida < 0)
            {
                defensor.Vida = 0;
                await EnviarAlCementerio(defensor.UsuarioPartidaID, defensor.Id, turnoId);
            }
                

            var cartaDefensaDB = await context.EstadosCarta.FirstAsync(c => c.Id == defensor.Id);
            cartaDefensaDB.Vida = defensor.Vida;

            var evento = new Evento
            {
                TurnoID = turnoId,
                EstadoCartaID = atacante.Id,
                Accion = "Ataque",
                Origen = atacante.Nombre,
                Destino = defensor.Nombre
            };

            var eventoDTO = new EventoDTO
            {
                Origen = atacante.Nombre,
                Destino = defensor.Nombre
            };

            context.Eventos.Add(evento);
            await context.SaveChangesAsync();

            return eventoDTO;
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

        public async Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId, string lugar, int turnoId)
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

            var cartaEnCampo = await CambiarPosicion(cartaSeleccionada.Id, lugar, turnoId, "Colocar en Campo");
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
            
            var usuarioPartida = await context.UsuariosPartida.FirstOrDefaultAsync(up => up.Id == usuarioPartidaId);

            if (usuarioPartida != null)
            {
                usuarioPartida.CartasPerdidas += 1;
                await context.SaveChangesAsync();
            }

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

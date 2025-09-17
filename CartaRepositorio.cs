using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class CartaRepositorio : Repositorio<Carta>, ICartaRepositorio
    {
        private readonly AppDbContext context;

        public CartaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<CartaDTO?> NombreCarta(string nombre)
        {
            var carta = await context.Cartas
                .Where(c => c.NombreCarta == nombre)
                .Select(c => new CartaDTO
                {
                    Id = c.Id,
                    NombreCarta = c.NombreCarta!,
                    TipoCarta = c.TipoCarta!,
                    NivelCarta = c.NivelCarta,
                    Ataque = c.Ataque,
                    Vida = c.Vida,
                    Velocidad = c.Velocidad
                })
                .FirstOrDefaultAsync();
            return carta;
        }

        public async Task<List<CartaDTO?>> NivelCarta(int nivel)
        {
            var lista = await context.Cartas
                .Where(c => c.NivelCarta == nivel)
                .Select(c => new CartaDTO
                {
                    Id = c.Id!,
                    NombreCarta = c.NombreCarta!,
                    TipoCarta = c.TipoCarta!,
                    NivelCarta = c.NivelCarta!,
                    Ataque = c.Ataque!,
                    Vida = c.Vida!,
                    Velocidad = c.Velocidad!
                })
                .ToListAsync();
            return lista;
        }
        public async Task<List<CartaDTO?>> ListaNombreCarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.NombreCarta)
                .Select(c => new CartaDTO
                {
                    Id = c.Id,
                    NombreCarta = c.NombreCarta!,
                    TipoCarta = c.TipoCarta!,
                    NivelCarta = c.NivelCarta!,
                    Ataque = c.Ataque!,
                    Vida = c.Vida!,
                    Velocidad = c.Velocidad!
                })
                .ToListAsync();

            return lista;
        }

        public async Task<List<CartaNumeroDTO?>> ListaATKCarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.Ataque)
                .Select(c => new CartaNumeroDTO
                {
                    Nombre = c.NombreCarta!,
                    Numero = c.Ataque!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaNumeroDTO?>> ListaVIDACarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.Vida)
                .Select(c => new CartaNumeroDTO
                {
                    Nombre = c.NombreCarta!,
                    Numero = c.Vida!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaNumeroDTO?>> ListaNivelCarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.NivelCarta)
                .Select(c => new CartaNumeroDTO
                {
                    Nombre = c.NombreCarta!,
                    Numero = c.NivelCarta
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaNumeroDTO?>> ListaVelocidadCarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.Velocidad)
                .Select(c => new CartaNumeroDTO
                {
                    Nombre = c.NombreCarta!,
                    Numero = c.Velocidad!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaPalabraDTO?>> ListaTipoCarta()
        {
            var lista = await context.Cartas
                .OrderBy(c => c.TipoCarta)
                .Select(c => new CartaPalabraDTO
                {
                    Nombre = c.NombreCarta!,
                    Palabra = c.TipoCarta!
                })
                .ToListAsync();
            return lista;
        }

        public async Task<List<CartaDTO?>> TipoCarta(string tipo)
        {
            var lista = await context.Cartas
                .Where(c => c.TipoCarta == tipo)
                .Select(c => new CartaDTO
                {
                    Id = c.Id,
                    NombreCarta = c.NombreCarta!,
                    TipoCarta = c.TipoCarta!,
                    NivelCarta = c.NivelCarta,
                    Ataque = c.Ataque,
                    Vida = c.Vida,
                    Velocidad = c.Velocidad
                })
                .ToListAsync();
            return lista;
        }


    }
}

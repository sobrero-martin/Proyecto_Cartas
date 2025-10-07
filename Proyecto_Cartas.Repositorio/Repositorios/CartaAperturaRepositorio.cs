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
    public class CartaAperturaRepositorio : Repositorio<CartaApertura>, ICartaAperturaRepositorio
    {
        private readonly AppDbContext context;

        public CartaAperturaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<CartaAperturaMostrarDTO>> GetListaApertura()
        {
            var lista = await context.CartasApertura
                 .Include(ca => ca.CartaSobre).ThenInclude(cs => cs.Carta)
                 .Include(ca => ca.CartaSobre).ThenInclude(cs => cs.Sobre)
                 .Select(ca => new CartaAperturaMostrarDTO
                 {
                     Id = ca.Id,
                     CompraSobreID = ca.CompraSobreID,
                     CartaSobreID = ca.CartaSobreID,
                     CantidadCartasObtenidas = ca.CantidadCartasObtenidas,
                     NombreCarta = ca.CartaSobre!.Carta!.NombreCarta,
                     NombreSobre = ca.CartaSobre!.Sobre!.NombreSobre
                 })
                 .ToListAsync();
            return lista;
        }

        public async Task<CartaAperturaMostrarDTO?> GetAperturaId(int id)
        {
            var entidad = await context.CartasApertura
                .Include(ca => ca.CartaSobre).ThenInclude(cs => cs.Carta)
                .Include(ca => ca.CartaSobre).ThenInclude(cs => cs.Sobre)
                .Where(ca => ca.Id == id)
                .Select(ca => new CartaAperturaMostrarDTO
                {
                    Id = ca.Id,
                    CompraSobreID = ca.CompraSobreID,
                    CartaSobreID = ca.CartaSobreID,
                    CantidadCartasObtenidas = ca.CantidadCartasObtenidas,
                    NombreCarta = ca.CartaSobre!.Carta!.NombreCarta,
                    NombreSobre = ca.CartaSobre!.Sobre!.NombreSobre
                })
                .FirstOrDefaultAsync();
            return entidad;
        }
    }
}

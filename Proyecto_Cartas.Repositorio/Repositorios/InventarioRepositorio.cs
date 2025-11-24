using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class InventarioRepositorio : Repositorio<Inventario>, IInventarioRepositorio
    {
        private readonly AppDbContext context;
        private readonly ICartaRepositorio cartaRepositorio;
        public InventarioRepositorio(AppDbContext context, ICartaRepositorio cartaRepositorio) : base(context)
        {
            this.context = context;
            this.cartaRepositorio = cartaRepositorio;
        }

        public async Task<bool> MazoInicial(int perfilUsuarioId, int opcion)
        {
            var yaTieneCartas = await context.Inventarios.AnyAsync(i => i.PerfilUsuarioID == perfilUsuarioId);
            if (yaTieneCartas)
            {
                return false;
            }

            List<Inventario> cartasIniciales = new List<Inventario>();
            if (opcion == 1)
            {
                CartaDTO? carta1 = await cartaRepositorio.NombreCarta("Rey Pepe");
                CartaDTO? carta2 = await cartaRepositorio.NombreCarta("Caballero Pepe");

                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta1!.Id, Tipo = "Mazo Inicial" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta2!.Id, Tipo = "Mazo Inicial" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta1!.Id, Tipo = "Inventario" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta2!.Id, Tipo = "Inventario" });
            }
            else if (opcion == 2)
            {
                CartaDTO? carta1 = await cartaRepositorio.NombreCarta("Rey Juan");
                CartaDTO? carta2 = await cartaRepositorio.NombreCarta("Caballero Juan");
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta1!.Id, Tipo = "Mazo Inicial" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta2!.Id, Tipo = "Mazo Inicial" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta1!.Id, Tipo = "Inventario" });
                cartasIniciales.Add(new Inventario { PerfilUsuarioID = perfilUsuarioId, CartaID = carta2!.Id, Tipo = "Inventario" });
            }

            await context.Inventarios.AddRangeAsync(cartasIniciales);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

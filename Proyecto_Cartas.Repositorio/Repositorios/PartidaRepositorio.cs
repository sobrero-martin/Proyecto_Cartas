using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class PartidaRepositorio : Repositorio<Partida>, IPartidaRepositorio
    {
        private readonly AppDbContext context;
        public PartidaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

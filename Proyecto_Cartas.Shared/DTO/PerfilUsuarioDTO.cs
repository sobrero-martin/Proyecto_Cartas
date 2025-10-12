using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class PerfilUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int Nivel { get; set; }
        public int Experiencia { get; set; }
        public int PartidasJugadas { get; set; }
        public int PartidasGanadas { get; set; }
        public string UltimoLogin { get; set; } = string.Empty;
    }
}

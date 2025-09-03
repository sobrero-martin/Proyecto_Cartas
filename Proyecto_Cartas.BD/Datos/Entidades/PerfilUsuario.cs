using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class PerfilUsuario : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "El nivel es requerido.")]
        public int Nivel { get; set; } = 1;

        [Required(ErrorMessage = "La experiencia es requerida.")]
        public int Experiencia { get; set; } = 0;

        [Required(ErrorMessage = "Las partidas jugadas son requeridas.")]
        public int PartidasJugadas { get; set; } = 0;

        [Required(ErrorMessage = "Las partidas ganadas son requeridas.")]
        public int PartidasGanadas { get; set; } = 0;

        [Required(ErrorMessage = "El último login es requerido.")]
        public DateTime UltimoLogin { get; set; } = DateTime.UtcNow;
    }
}

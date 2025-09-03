using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class ConfiguracionUsuario : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "El volumen de musica es requerido.")]
        public int VolumenMusica { get; set; } = 50;

        [Required(ErrorMessage = "El volumen de sfx es requerido.")]
        public int VolumenSFX { get; set; } = 50;
    }
}

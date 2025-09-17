using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class ConfiguracionUsuarioDTO
    {
        public int UsuarioID { get; set; }
        public  int VolumenMusica { get; set; } 
        public  int VolumenSFX { get; set; } 
    }
}

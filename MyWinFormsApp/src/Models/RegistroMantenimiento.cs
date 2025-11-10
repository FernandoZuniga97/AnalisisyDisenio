using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWinFormsApp.src.Models
{
    public class RegistroMantenimiento
    {
        public string ID { get; set; }

        public string Fecha { get; set; }

        public string Dispositivo { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public string Tecnico { get; set; }

        public string Materiales { get; set; }

        public string Costo { get; set; }

        public string Observaciones { get; set; }
    }
}

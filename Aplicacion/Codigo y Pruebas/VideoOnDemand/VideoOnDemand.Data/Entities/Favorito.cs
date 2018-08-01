using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public class Favorito
    {
        public int? Id { get; set; }
        public DateTime? FechaAgregado { get; set; }
     
        public Media Media { get; set; }
        public int? MediaId { get; set; }

        public Usuario Usuario { get; set; }
        public int? UsuarioId { get; set; }

    }
}

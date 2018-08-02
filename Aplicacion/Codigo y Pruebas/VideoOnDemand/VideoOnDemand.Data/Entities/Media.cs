using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOnDemand.Entities
{
    public abstract class Media
    {
        public int? Id {get; set;}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
        public DateTime? FechaDeLanzamiento { get; set; }

        public EEstatusMedia? Estatus { get; set; }
        public ICollection<Genero> Generos { get; set; }
        public ICollection<Persona> Actores { get; set; }
       

    }
}

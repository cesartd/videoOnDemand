using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class EpisodioViewModel
    {
        public int? Temporada { get; set; }

        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
        public DateTime? FechaDeLanzamiento { get; set; }

        public int? Estatus { get; set; }
        /*public ICollection<GeneroViewModel> Generos { get; set; }
        public ICollection<PersonaViewModel> Actores { get; set; }*/
    }
}
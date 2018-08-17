using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class MediaActorGeneroViewModel
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaDeRegistro { get; set; }

        public DateTime? FechaDeLanzamiento { get; set; }

        public int? Estatus { get; set; }
        public ICollection<GeneroViewModel> Generos { get; set; }
        public ICollection<PersonaViewModel> Actores { get; set; }
    }

    public class SerieActorGeneroViewModel
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public DateTime? FechaDeRegistro { get; set; }
        
        public DateTime? FechaDeLanzamiento { get; set; }

        public  int? Estatus { get; set; }
        public ICollection<GeneroViewModel> Generos { get; set; }
        public ICollection<PersonaViewModel> Actores { get; set; }
    }

}
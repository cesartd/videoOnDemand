using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class EpisodioViewModel
    {
        public int? Temporada { get; set; }

        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeLanzamiento { get; set; }

        public int? SerieId { get; set; }

        public int? Estatus { get; set; }
        /*public ICollection<GeneroViewModel> Generos { get; set; }
        public ICollection<PersonaViewModel> Actores { get; set; }*/
    }
}
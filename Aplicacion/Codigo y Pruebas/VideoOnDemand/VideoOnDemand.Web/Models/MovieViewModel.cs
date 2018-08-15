using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public EEstatusMedia? Estatus { get; set; }
        //mantiene la lista  de generos completa

        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario

        [Required]
        public int[] GenerosSeleccionados{ get; set; }

        //mantiene la lista  de generos completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario

        [Required]
        public int[] ActoresSeleccionados { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [Required]
        public DateTime? FechaDeLanzamiento { get; set; }

    }
}
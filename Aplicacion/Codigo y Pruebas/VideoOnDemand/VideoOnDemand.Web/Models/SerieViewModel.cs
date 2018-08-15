using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel
    {
        public ICollection<EpisodioViewModel> Episodios { get; set; }
        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Duración")]
        public int? DuracionMin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de registro")]
        public DateTime? FechaDeRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de lanzamiento")]
        public DateTime? FechaDeLanzamiento { get; set; }

        public int? Estatus { get; set; }
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] ActoresSeleccionados { get; set; }
    }
}
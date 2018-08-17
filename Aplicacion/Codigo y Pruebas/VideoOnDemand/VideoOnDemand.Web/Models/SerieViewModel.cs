using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel
    {
        public bool? isAdded = false;
        public ICollection<EpisodioViewModel> Episodios { get; set; }
        public int? Id { get; set; }
        [Required (ErrorMessage ="Nombre Requerido!")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required (ErrorMessage = "Duración Requerido!")]
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

        [Required(ErrorMessage = "Estado Requerido!")]
        public int? Estatus { get; set; }
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }

        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] ActoresSeleccionados { get; set; }
    }


}
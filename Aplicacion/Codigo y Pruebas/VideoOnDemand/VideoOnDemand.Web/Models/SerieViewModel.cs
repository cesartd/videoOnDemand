﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoOnDemand.Web.Models
{
    public class SerieViewModel
    {
        public ICollection<EpisodioViewModel> Episodios { get; set; }
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

        public int? Estatus { get; set; }
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        public int[] GenerosSeleccionados { get; set; }
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] ActoresSeleccionados { get; set; }
    }


}
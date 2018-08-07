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
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMin { get; set; }
        public EEstatusMedia? Estatus { get; set; }
        //mantiene la lista  de generos completa
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] GenerosSeleccionados{ get; set; }

        //mantiene la lista  de generos completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] ActoresSeleccionados { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeRegistro { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeLanzamiento { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MovieViewModel:Media
    {
        //mantiene la lista  de generos completa
        public ICollection<GeneroViewModel> GenerosDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] GenerosSeleccionados{ get; set; }

        //mantiene la lista  de generos completa
        public ICollection<PersonaViewModel> ActoresDisponibles { get; set; }
        //mantiene los generos que seleccione el usuario
        public int[] ActoresSeleccionados { get; set; }

    }
}
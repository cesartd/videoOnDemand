using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class TemporadaViewModel
    {
        public int? Temporada { get; set; }
        public List<EpisodioViewModel> Episodios { get; set;}
    }
}
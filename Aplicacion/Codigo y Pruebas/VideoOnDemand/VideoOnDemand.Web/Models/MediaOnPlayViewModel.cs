using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class MediaOnPlayViewModel
    {
        public int? Id { get; set; }
        public long Milisegundo { get; set; }

        public Media Media { get; set; }
        public int? MediaId { get; set; }

        public Usuario Usuario { get; set; }
        public int? UsuarioId { get; set; }

        public bool? Activo { get; set; }
    }
}
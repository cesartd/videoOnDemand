using System;
using VideoOnDemand.Entities;

namespace VideoOnDemand.Web.Models
{
    public class FavoritoViewModel
    {
        public int? Id { get; set; }
        public DateTime? FechaAgregado { get; set; }

        public Media Media { get; set; }
        public int? MediaId { get; set; }

        public Usuario Usuario { get; set; }
        public int? UsuarioId { get; set; }
    }
}
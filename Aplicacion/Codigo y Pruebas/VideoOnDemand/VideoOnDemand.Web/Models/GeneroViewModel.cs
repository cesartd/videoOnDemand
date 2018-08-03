using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class GeneroViewModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        public string Descripcion { get; set; }

        public bool? Activo { get; set; }

    }
}
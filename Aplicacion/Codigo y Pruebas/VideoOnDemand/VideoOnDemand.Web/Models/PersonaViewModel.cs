using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }

        public bool? Activo { get; set; }
        
    }
}
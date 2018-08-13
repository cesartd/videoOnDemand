using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace VideoOnDemand.Web.Models
{
    public class PersonaViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name="Descripción")]
        public string Descripcion { get; set; }
        [Display(Name="Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }
        [Display(Name ="Estado")]
        
        public bool? Activo { get; set; }
        
    }
}
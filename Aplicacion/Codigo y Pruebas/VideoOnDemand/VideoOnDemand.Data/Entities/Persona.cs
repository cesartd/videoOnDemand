using System;

namespace VideoOnDemand.Entities
{
    public class Persona
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaNacimiento { get; set; }

    }
}

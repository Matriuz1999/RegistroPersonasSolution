using System;
using System.Collections.Generic;

namespace Models
{
    public class Persona
    {
        public string DocumentoIdentidad { get; set; } 
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public List<string> Telefonos { get; set; }
        public List<string> Correos { get; set; }
        public List<string> Direcciones { get; set; }
    }
}

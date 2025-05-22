using Data;
using Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Business
{
    public class PersonaService
    {
        private readonly PersonaRepository _repo;

        public void RegistrarPersona(Persona persona)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(persona.DocumentoIdentidad) ||
                string.IsNullOrWhiteSpace(persona.Nombres) ||
                string.IsNullOrWhiteSpace(persona.Apellidos) ||
                persona.FechaNacimiento == default)
                throw new Exception("Datos obligatorios faltantes.");

            if (!Regex.IsMatch(persona.DocumentoIdentidad, @"^[a-zA-Z0-9]+$"))
                throw new Exception("El documento solo debe contener caracteres alfanuméricos.");

            if (!Regex.IsMatch(persona.Nombres, @"^[a-zA-Z\s]+$") || !Regex.IsMatch(persona.Apellidos, @"^[a-zA-Z\s]+$"))
                throw new Exception("Nombres y apellidos solo pueden contener letras.");

            if (_repo.ExisteDocumento(persona.DocumentoIdentidad))
                throw new Exception("Ya existe una persona con ese documento.");

            if ((persona.Correos.Count + persona.Direcciones.Count) < 1)
                throw new Exception("Debe registrar al menos un contacto (correo o dirección).");

            if (persona.Telefonos.Count > 2 || persona.Correos.Count > 2 || persona.Direcciones.Count > 2)
                throw new Exception("Máximo 2 elementos por tipo de contacto.");

            _repo.RegistrarPersona(persona);
        }
    }
}

using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFService.Interface;

namespace WCFService.Service
{
    public class PersonaServiceWCF : IPersonaService
    {
        private readonly PersonaService _service;

        public void RegistrarPersona(Persona persona)
        {
            try
            {
                _service.RegistrarPersona(persona);
            }
            catch (System.Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Interface
{
    [ServiceContract]
    public interface IPersonaService
    {
        [OperationContract]
        [FaultContract(typeof(string))]
        void RegistrarPersona(Persona persona);
    }
}
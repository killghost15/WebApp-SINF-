using Interop.GcpBE900;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class EncomendaDeClientesPckController : ApiController
    {
        //
        // GET: //

        public IEnumerable<Lib_Primavera.Model.DocVendaPCK> Get(string tipoDoc, string serie, string estado)
        {
            return Lib_Primavera.PriIntegration.Encomendas_List_PCK(tipoDoc, serie, estado);
        }
    }
}

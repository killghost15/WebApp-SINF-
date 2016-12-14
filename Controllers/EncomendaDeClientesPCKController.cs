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
        // GET: /api/encomendadeclientespck?tipoDoc=ECL&serie=2016&estado=P

        public IEnumerable<Lib_Primavera.Model.DocVendaPCK> Get(string tipoDoc, string serie, string estado, int picking)
        {
            return Lib_Primavera.PriIntegration.Encomendas_List_PCK(tipoDoc, serie, estado, picking);
        }

        // GET: /api/encomendadeclientespck?id=blabla

        public IEnumerable<Lib_Primavera.Model.DocVendaPCK> Get(string id)
        {
            return Lib_Primavera.PriIntegration.Encomendas_List_PCK(id);
        }

        public string GetTotalPrice(string id)
        {
            return Lib_Primavera.PriIntegration.Encomendas_List_PCK_TotalPrice(id);
        }

        public List<string> GetSerie(string tipoDoc)
        {
            return Lib_Primavera.PriIntegration.EncomendaSerie(tipoDoc);
        }

        public List<string> GetSerie()
        {
            return Lib_Primavera.PriIntegration.EncomendaSerie();
        }

    }
}

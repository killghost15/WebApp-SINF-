using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class PickingOrderController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.DocVendaPCK> Post(string tipoDoc, string serie, int numOrders)
        {
            return Lib_Primavera.PriIntegration.PickingOrder(tipoDoc, serie, numOrders);
        }
    }
}

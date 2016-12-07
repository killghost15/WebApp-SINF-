using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class LocalizacoesArmazemController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.LocalizacaoArmazem> Get()
        {
            return Lib_Primavera.PriIntegration.ListaLocalizacoesArmazem();
        }
    }
}

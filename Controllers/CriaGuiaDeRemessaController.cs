using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class CriaGuiaDeRemessaController : ApiController
    {
        public int Post(Lib_Primavera.Model.CriaGuiaDeRemessa gr)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.GuiaDeRemessa_New(gr);

            if (erro.Erro == 0)
            {
                String a = "File was processed.";
                return 0;
            }

            else
            {
                return 1;
            }

        }
    }
}

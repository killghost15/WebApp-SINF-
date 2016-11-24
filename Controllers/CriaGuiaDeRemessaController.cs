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
        public HttpResponseMessage Post(Lib_Primavera.Model.CriaGuiaDeRemessa gr)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.GuiaDeRemessa_New(gr);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, gr.Documento);
                string uri = Url.Link("DefaultApi", new { DocId = gr.Documento });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }

        }
    }
}

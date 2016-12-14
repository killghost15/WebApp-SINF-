using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class TransfItemPckAreaController : ApiController
    {
        public HttpResponseMessage Post(Lib_Primavera.Model.TransItemPckArea artigo)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.TransfereItemPickingArea(artigo);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, artigo.Artigo.CodArtigo);
                string uri = Url.Link("DefaultApi", new { CodArtigo = artigo.Artigo.CodArtigo });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }

        }

        public List<string> GetSerie(string tipoDoc)
        {
            return Lib_Primavera.PriIntegration.TransferenciaSerie(tipoDoc);
        }

        public IList<FirstREST.Lib_Primavera.Model.TransferenciasObj> GetTransferencias(string tipoDoc)
        {
            return Lib_Primavera.PriIntegration.TransferenciaNumsSerie(tipoDoc);
        }

        public IList<FirstREST.Lib_Primavera.Model.TransferenciasObj> GetTransferenciasLinhas(string tipoDoc, string serie, int num)
        {
            return Lib_Primavera.PriIntegration.TransferenciaNumsSerie(tipoDoc);
        }
    }
}

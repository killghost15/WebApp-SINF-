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
    public class ArtigoArmazemController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.ArtigoArmazem> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArtigoArmazem();
        }

         public HttpResponseMessage Post(Lib_Primavera.Model.ArtigoArmazem art)
         {
             Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
             erro = Lib_Primavera.PriIntegration.TransfereArtigo(art);

             if (erro.Erro == 0)
             {
                 var response = Request.CreateResponse(
                    HttpStatusCode.Created, art);
                 string uri = Url.Link("DefaultApi", new {  CodArtigo = art.ArtigoId });
                 response.Headers.Location = new Uri(uri);
                 return response;
             }

             else
             {
                 return Request.CreateResponse(HttpStatusCode.BadRequest);
             }

         }

    }
}

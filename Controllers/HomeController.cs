using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("/Views/Home/Index.cshtml");
        }

        public ActionResult TransferenciaArmazem()
        {
            ArtigoArmazemController cont = new ArtigoArmazemController();
            
            ViewBag.artigosArmazem = cont.Get();
            var lista_artigos = ViewBag.artigosArmazem;

            string artigos_id = "";
            string artigos_localizacao = "";

            foreach (var artigo in lista_artigos) {
                artigos_id = artigos_id + artigo.ArtigoId + ",";
                artigos_localizacao = artigos_localizacao + artigo.Localizacao + ",";
            }

            ViewBag.artigosId = artigos_id;
            ViewBag.artigosLocalizacao = artigos_localizacao;

            /* TODO
             * -> ter uma lista com todas as localizaçoes
             * -> comparar essa lista com artigos_localizacao e retornar uma lista nova com apenas as localizaçoes vazias
             * -> No Javascript:
             *    -> crio uma lista com localizaçoes que estão no 2º dropdown, mas retiro a que foi selecionada
             *    -> a essa lista faço append das localizações vazias
             */

            return View("/Views/Home/Transferencia_Armazem.cshtml");
        }

        public ActionResult Encomendas(string tipoDoc, string serie, string estado)
        {
            EncomendaDeClientesPckController cont = new EncomendaDeClientesPckController();
            ViewBag.encomendas = cont.Get(tipoDoc, serie, estado);
            return View("/Views/Home/Encomendas.cshtml");
        }

        public ActionResult Encomenda(string id)
        {
            EncomendaDeClientesPckController cont = new EncomendaDeClientesPckController();
            ViewBag.encomenda = cont.Get(id);
            return View("/Views/Home/Encomenda.cshtml");
        }
    }
}

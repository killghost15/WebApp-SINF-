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
            LocalizacoesArmazemController cont2 = new LocalizacoesArmazemController();
            
            ViewBag.artigosArmazem = cont.Get();
            var lista_artigos = ViewBag.artigosArmazem;

            string artigosId_string = "";
            string artigosLocalizacao_string = "";

            foreach (var artigo in lista_artigos) {
                artigosId_string = artigosId_string + artigo.ArtigoId + ",";
                artigosLocalizacao_string = artigosLocalizacao_string + artigo.Localizacao + ",";
            }

            ViewBag.artigosId = artigosId_string;
            ViewBag.artigosLocalizacao = artigosLocalizacao_string;

            // Lista de todas as localizacoes existentes
            ViewBag.localizacoesArmazem = cont2.Get();
            var lista_localizacoes = ViewBag.localizacoesArmazem;

            string localizacoesLocalizacao_string = "";

            foreach (var localizacao in lista_localizacoes)
            {
                localizacoesLocalizacao_string = localizacoesLocalizacao_string + localizacao.Localizacao + ",";
            }

            ViewBag.localizacoesLocalizacao = localizacoesLocalizacao_string;

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
            ViewBag.precoTotal = cont.GetTotalPrice(id);
            return View("/Views/Home/Encomenda.cshtml");
        }

        public ActionResult Login()
        {   
            if(Session["LoggedIn"] == null) {
                return View("/Views/Home/Login.cshtml");
            }
            return View("/Views/Home/Index.cshtml");
        }

        [System.Web.Mvc.HttpPost]
        public void Index(String Email, String Password)
        {
            FuncionariosController cont = new FuncionariosController();
            IEnumerable<Lib_Primavera.Model.Funcionario> funcs = cont.Get();

            bool found  = false;
            string nome="";

            foreach (var func in funcs)
            {
                if (func.Email.Equals(Email) && func.Password.Equals(Password))
                {
                    nome = func.Nome;
                    found = true;
                }
            }

            ViewBag.Email = Email;
            ViewBag.Password = Password;

            if (!found)
                Response.Redirect("/Home/Login");
            else
            {
                Session.Add("email", Email);
                Session.Add("name", nome);
                Session["LoggedIn"] = "1";
                Response.Redirect("/Home/Index",false);
            }
        }

    }
}

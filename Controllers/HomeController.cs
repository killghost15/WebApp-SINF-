using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace FirstREST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["LoggedIn"] == null)
            {
                return View("/Views/Home/Login.cshtml");
            }
            return View("/Views/Home/Index.cshtml");
        }

        public ActionResult TransferenciaArmazem(string tipoDoc)
        {
            ArtigoArmazemController cont = new ArtigoArmazemController();
            LocalizacoesArmazemController cont2 = new LocalizacoesArmazemController();

            TransfItemPckAreaController cont3 = new TransfItemPckAreaController();
            ViewBag.test1 = cont3.GetSerie(tipoDoc);
            
            ViewBag.artigosArmazem = cont.Get();
            var lista_artigos = ViewBag.artigosArmazem;

            string artigosId_string = "";
            string artigosLocalizacao_string = "";

            foreach (var artigo in lista_artigos) {
                artigosId_string = artigosId_string + artigo.CodArtigo + ",";
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

        public ActionResult Documents(string tipoDoc, string serie, string estado)
        {
            EncomendaDeClientesPckController cont = new EncomendaDeClientesPckController();
            ViewBag.encomendas = cont.Get(tipoDoc, serie, estado);
            return View("/Views/Home/Documents.cshtml");
        }

        public ActionResult Transferencias(string tipoDoc)
        {
            TransfItemPckAreaController cont = new TransfItemPckAreaController();
            ViewBag.test2 = cont.GetTransferencias(tipoDoc);
            return View("/Views/Home/View_Transferencias.cshtml");
        }

        public ActionResult TransferenciasLinhas(string tipoDoc, string serie, int num)
        {
            TransfItemPckAreaController trns = new TransfItemPckAreaController();
            ViewBag.test3 = trns.GetTransferenciasLinhas(tipoDoc, serie, num);
            return View("/Views/Home/LinhasTransferencia.cshtml");
        }

        public ActionResult Choose_Documents()
        {
            EncomendaDeClientesPckController cont = new EncomendaDeClientesPckController();
            ViewBag.test = cont.GetSerie();
            return View("/Views/Home/Choose_Documents.cshtml");
        }

        public ActionResult Artigos()
        {
            ArtigosController cont = new ArtigosController();
            var lista_artigos = cont.Get();
            ViewBag.artigos = lista_artigos;

            ArtigoArmazemController cont2 = new ArtigoArmazemController();
            var lista_artigoArmazem = cont2.Get();
            var lista_localizacoesReal = new List<string>();

            foreach (var artigo in lista_artigos)
            {
                var lista_localizacoesTemp = new List<string>();

                foreach (var artigoArmazem in lista_artigoArmazem)
                {
                    if (artigo.CodArtigo == artigoArmazem.CodArtigo)
                    {
                        lista_localizacoesTemp.Add(artigoArmazem.Localizacao);
                    }
                }

                StringBuilder localizacoes_stringBuilder = new StringBuilder();

                Console.Write(lista_localizacoesTemp.Count());
                Console.Write(lista_artigos.Count());

                for (var i = 0; i < lista_localizacoesTemp.Count(); i++)
                {
                    localizacoes_stringBuilder.Append(lista_localizacoesTemp[i]);
                    if ((i + 1) != lista_localizacoesTemp.Count())
                        localizacoes_stringBuilder.Append("; ");
                }

                string localizacoes_string = localizacoes_stringBuilder.ToString();
                lista_localizacoesReal.Add(localizacoes_string);
            }

            ViewBag.localizacoesReal = lista_localizacoesReal;

            return View("/Views/Home/Artigos.cshtml");
        }

        public ActionResult Document(string id)
        {
            EncomendaDeClientesPckController cont = new EncomendaDeClientesPckController();
            ViewBag.encomenda = cont.Get(id);
            ViewBag.precoTotal = cont.GetTotalPrice(id);
            return View("/Views/Home/Document.cshtml");
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

        [System.Web.Mvc.HttpPost]
        public void Transferencia(string Artigo, string Localizacao_Origem, string Localizacao_Destino, string Quantidade, string TipoDoc, string Serie)
        {
            ArtigosController cont = new ArtigosController();
            var lista_artigos = cont.Get();

            // Número de artigos distintos no armazém
            int listaArtigos_length = 0;
            foreach (var artigo in lista_artigos)
            {
                listaArtigos_length++;
            }

            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            Lib_Primavera.Model.TransItemPckArea transferencia = new Lib_Primavera.Model.TransItemPckArea();

            // Percorre a lista de artigos até encontrar um artigo com o Id passado como argumento desta função
            foreach (var artigo in lista_artigos)
            {
                if (artigo.CodArtigo == Artigo)
                    transferencia.Artigo = artigo;
            }

            transferencia.TipoDoc = TipoDoc;
            transferencia.Serie = Serie;
            transferencia.LocalizacaoOrigem = Localizacao_Origem;
            transferencia.ArmazemOrigem = Localizacao_Origem[0] + "" + Localizacao_Origem[1];

            transferencia.LocalizacaoDestino = Localizacao_Destino;
            transferencia.ArmazemDestino = Localizacao_Destino[0] + "" + Localizacao_Destino[1];

            transferencia.Quantidade = Int32.Parse(Quantidade);

            erro = Lib_Primavera.PriIntegration.TransfereItemPickingArea(transferencia);

            Console.Write(Artigo);
            Response.Redirect("/Home");
        }

    }
}

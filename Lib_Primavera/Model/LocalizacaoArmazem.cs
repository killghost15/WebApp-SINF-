using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Lib_Primavera.Model
{
    public class LocalizacaoArmazem
    {
        public string Localizacao { get; set; }

        public string Armazem { get; set; }

        public string Descricao { get; set; }
    }
}
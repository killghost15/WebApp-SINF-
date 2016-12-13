using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Lib_Primavera.Model
{
    public class ArtigoArmazem
    {
        public string Localizacao { get; set; }

        public double StockAtual { get; set; }

        public string CodArtigo { get; set; }

        public string Armazem { get; set; }
    }
}

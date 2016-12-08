using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Artigo
    {
        public string CodArtigo { get; set; }

        public string DescArtigo { get; set; }

        public string STKMinimo { get; set; }

        public string STKMaximo { get; set; }

        public string STKAtual { get; set; }
        public string localizacao
        {
            get;
            set;
        }
        public int Quantidade
        {
            get;
            set;
        }
    }
}
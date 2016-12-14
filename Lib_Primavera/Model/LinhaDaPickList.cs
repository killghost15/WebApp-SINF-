using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class LinhaDaPickList
    {
        public string CodArtigo
        {
            get;
            set;
        }




        public double Quantidade { get; set; }

        public string Localizacao { get; set; }

        public string IdLinha { get; set; }
    }
}
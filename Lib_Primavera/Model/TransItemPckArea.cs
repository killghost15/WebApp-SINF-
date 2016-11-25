using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class TransItemPckArea
    {
        public string TipoDoc { get; set; }

        public string TipoEntidade { get; set; }

        public Artigo Artigo { get; set; }

        public string serie { get; set; }

        public string ArmazemEntrada { get; set; }

        public string ArmazemSaida { get; set; }

        public string LocalizacaoEntrada { get; set; }

        public string LocalizacaoSaida { get; set; }

        //public string LocalizacaoOrigem { get; set; }

        public double Quantidade { get; set; }
    }
}
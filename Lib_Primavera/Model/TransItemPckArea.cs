using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class TransItemPckArea
    {
        public Artigo Artigo { get; set; }

        public string ArmazemOrigem { get; set; }

        public string ArmazemDestino { get; set; }

        public string LocalizacaoOrigem { get; set; }

        public string LocalizacaoDestino { get; set; }

        //public string LocalizacaoOrigem { get; set; }

        public int Quantidade { get; set; }
    }
}
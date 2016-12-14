using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class LinhasStk
    {
        public string TipoDoc { get; set; }
        public int NumDoc { get; set; }
        public int NumLinha { get; set; }
        public double Quantidade { get; set; }
        public string Data { get; set; }
        public string Armazem { get; set; }
        public string Localizacao { get; set; }
        public string LocalizacaoOrigem { get; set; }
        public string Lote { get; set; }
        public string EntradaSaida { get; set; }
        public string Serie { get; set; }
        public string Descricao { get; set; }
    }
}
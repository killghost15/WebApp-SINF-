using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Lib_Primavera.Model
{
    public class Encomenda 
    {

        public int Data
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public List<Model.Artigo> lista
        {
            get;
            set;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Lib_Primavera.Model
{
    public class Encomenda 
    {

        public DateTime Data
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public List<Model.LinhaDaPickList> lista
        {
            get;
            set;
        }

    }
}

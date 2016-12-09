using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class LinhaDocPicking
    {
        public string LinhaId
        {
            get;
            set;
        }

        public LinhaDocVendaPCK linhaDocVenda
        {
            get;
            set;
        }
    }
}
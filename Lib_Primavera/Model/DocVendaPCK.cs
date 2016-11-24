using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class DocVendaPCK
    {
        public string Id
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }

        public string Entidade
        {
            get;
            set;
        }

        public string TipoDoc
        {
            get;
            set;
        }

        public string NumDoc
        {
            get;
            set;
        }

        public string DataCarga
        {
            get;
            set;
        }

        public string HoraCarga
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public string Documento
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public List<Model.LinhaDocVendaPCK> LinhasDoc
        {
            get;
            set;
        }
    }
}
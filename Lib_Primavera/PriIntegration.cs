﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using ADODB;
using System.Text;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {

        # region Funcionario

        public static List<Model.Funcionario> GetFuncionarios()
        {
            StdBELista objListFunc;
            Model.Funcionario func = new Model.Funcionario();
            List<Model.Funcionario> listfunc = new List<Model.Funcionario>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                sql.Append("SELECT Nome, Email, Password FROM Funcionarios");

                query = sql.ToString();

                objListFunc = PriEngine.Engine.Consulta(query);
                while (!objListFunc.NoFim())
                {
                    func = new Model.Funcionario();

                    func.Nome = objListFunc.Valor("Nome");
                    func.Email = objListFunc.Valor("Email");
                    func.Password = objListFunc.Valor("Password");

                    listfunc.Add(func);
                    objListFunc.Seguinte();
                }
            }

            return listfunc;
        }

        # endregion

        # region Cliente

        /* Função que devolve uma lista com todos os clientes */
        public static List<Model.Cliente> ListaClientes()
        {
            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        /* Função que devolve um cliente com um código codCliente */
        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            GcpBECliente objCli = new GcpBECliente();

            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        /* Função que faz update de um cliente */
        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            GcpBECliente objCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    
                    return erro;
                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        /* Função que faz delete de um cliente */
        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {
                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }
        
        /* Função que adiciona um novo cliente */
        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------

        #region Artigo

        /* Função que devolve uma lista com todos os artigos */
        public static List<Model.Artigo> ListaArtigos()
        {
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                //objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                sql.Append("select Artigo, Descricao, STKMinimo, STKMaximo, STKActual from Artigo");

                query = sql.ToString();
                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("Artigo");
                    art.DescArtigo = objList.Valor("Descricao");
                    art.STKMinimo = objList.Valor("STKMinimo");
                    art.STKMaximo = objList.Valor("STKMaximo");
                    art.STKAtual = objList.Valor("STKActual");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;
            }

        }

        /* Função que devolve um artigo com codArtigo */
        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        #endregion Artigo

        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }

            return listdc;
        }


        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";

                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra

        #region DocsVenda

        /* Função que permite criar uma nova encomenda */
        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();
            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();
            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();
            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }

                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";

                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;

                return erro;
            }
        }

        /* Função que devolve uma lista com todas as encomendas */
        public static List<Model.DocVenda> Encomendas_List()
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }

            return listdv;
        }

        /* Função que devolve uma encomenda com numdoc */
        public static Model.DocVenda Encomenda_Get(string numdoc)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;

                return dv;
            }

            return null;
        }


        #endregion DocsVenda

        #region TransferenciaArmazem

        /* Função que efetua uma transferência de armazém de um certo artigo art */
        public static Lib_Primavera.Model.RespostaErro TransfereArtigo(Model.ArtigoArmazem art)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

            GcpBEArtigoArmazem myart = new GcpBEArtigoArmazem();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    myart.set_Artigo(art.CodArtigo);
                    myart.set_Armazem(art.Armazem);
                    myart.set_Localizacao(art.Localizacao);
                    myart.set_StkActual(art.StockAtual);
                    PriEngine.Engine.Comercial.ArtigosArmazens.Actualiza(myart);
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";

                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        /* Função que devolve uma lista com todos os artigos que estão no armazém */
        public static List<Model.ArtigoArmazem> ListaArtigoArmazem()
        {
            StdBELista objList;

            List<Model.ArtigoArmazem> listartigos = new List<Model.ArtigoArmazem>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("select Artigo, Armazem, StkActual, Localizacao from ArtigoArmazem");

                while (!objList.NoFim())
                {
                    listartigos.Add(new Model.ArtigoArmazem
                    {
                        Localizacao = objList.Valor("Localizacao"),
                        CodArtigo = objList.Valor("Artigo"),
                        Armazem = objList.Valor("Armazem"),
                        StockAtual = objList.Valor("StkActual")
                    });

                    objList.Seguinte();
                }

                return listartigos;
            }
            else
                return null;
        }

        /* public static Model.ArtigoArmazem GetArtigoArmazem(string id)
        {
            GcpBEArtigoArmazem objartigo = new GcpBEArtigoArmazem();


            Model.ArtigoArmazem myart = new Model.ArtigoArmazem();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.ArtigosArmazens.Existe(id) == true)
                {
                    objartigo = PriEngine.Engine.Comercial.
                    myart.CodCliente = objartigo.get_Cliente();
                    myart.NomeCliente = objartigo.get_Nome();
                    myart.Moeda = objartigo.get_Moeda();
                    myart.NumContribuinte = objartigo.get_NumContribuinte();
                    myart.Morada = objartigo.get_Morada();
                    return myart;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }*/

        /* Função que devolve uma lista com todas as localizações que existem nos armazéns */
        public static List<Model.LocalizacaoArmazem> ListaLocalizacoesArmazem()
        {
            StdBELista objList;

            List<Model.LocalizacaoArmazem> listlocalizacoes = new List<Model.LocalizacaoArmazem>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("select Localizacao, Armazem, Descricao from ArmazemLocalizacoes");

                while (!objList.NoFim())
                {
                    listlocalizacoes.Add(new Model.LocalizacaoArmazem
                    {
                        Localizacao = objList.Valor("Localizacao"),
                        Armazem = objList.Valor("Armazem"),
                        Descricao = objList.Valor("Descricao")
                    });

                    objList.Seguinte();
                }

                return listlocalizacoes;
            }
            else
                return null;
        }


        #endregion TransferenciaArmazem

        #region PickingAlternativo

        public static List<Model.DocVendaPCK> PickingOrder(string tipoDoc, string serie, int numOrders)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            String estado = "P";
            Model.DocVendaPCK dv = new Model.DocVendaPCK();
            List<Model.DocVendaPCK> listdv = new List<Model.DocVendaPCK>();
            Model.LinhaDocVendaPCK lindv = new Model.LinhaDocVendaPCK();
            List<Model.LinhaDocVendaPCK> listlindv = new List<Model.LinhaDocVendaPCK>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                string a = "SELECT top " + numOrders + " Id, Data, Entidade, TipoDoc, NumDoc, DataCarga, HoraCarga, Serie, Documento, Estado FROM cabecdoc CD inner join CabecDocStatus ST ON CD.id = ST.IdCabecDoc";
                sql.Append(a);
                sql.Append(" WHERE TipoDoc='@1@'");
                sql.Append(" AND Serie='@2@'");
                sql.Append(" AND Estado='@3@' and Picking = 0 order by NumDoc, HoraCarga asc ");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);
                query = query.Replace("@2@", serie);
                query = query.Replace("@3@", estado);
                //query = query.Replace("@4@", numOrders.ToString());

                objListCab = PriEngine.Engine.Consulta(query);
                while (!objListCab.NoFim())
                {
                    GcpBEDocumentoVenda docv = new GcpBEDocumentoVenda();                    
                    dv = new Model.DocVendaPCK();
                    dv.Id = objListCab.Valor("Id");

                    DateTime dt = objListCab.Valor("Data");
                    dv.Data = dt.ToString("dd-MM-yyyy");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    int numDoc = objListCab.Valor("NumDoc");
                    dv.NumDoc = Convert.ToString(numDoc);
                    dv.DataCarga = objListCab.Valor("DataCarga");
                    dv.HoraCarga = objListCab.Valor("HoraCarga");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.Documento = objListCab.Valor("Documento");
                    dv.Estado = objListCab.Valor("Estado");
                    objListLin = PriEngine.Engine.Consulta("Select IdCabecDoc, NumLinha, Artigo, LD.Quantidade, Seccao, Armazem, Localizacao, Lote, Descricao, Id, Unidade, QuantTrans, EstadoTrans, PrecUnit,(PrecUnit * LD.Quantidade+ (PrecUnit*LD.Quantidade*(TaxaIva/100))) as PrecoTotal from LinhasDoc LD inner join LinhasDocStatus LDS ON LD.id = LDS.IdLinhasDoc AND IdCabecDoc='" + dv.Id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVendaPCK>();

                    string filial = "000";
                    PriEngine.Engine.Comercial.Vendas.ActualizaValorAtributo(filial, dv.TipoDoc, dv.Serie, numDoc, "Picking", 1);

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVendaPCK();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        int numLinha = objListLin.Valor("NumLinha");
                        lindv.NumLinha = Convert.ToString(numLinha);
                        lindv.Artigo = objListLin.Valor("Artigo");
                        double quantidade = objListLin.Valor("Quantidade");
                        lindv.Quantidade = Convert.ToString(quantidade);
                        lindv.Seccao = objListLin.Valor("Seccao");
                        lindv.Armazem = objListLin.Valor("Armazem");
                        lindv.Localizacao = objListLin.Valor("Localizacao");
                        lindv.Lote = objListLin.Valor("Lote");
                        lindv.Descricao = objListLin.Valor("Descricao");
                        lindv.Id = objListLin.Valor("Id");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        double quantTrans = objListLin.Valor("QuantTrans");
                        lindv.QuantTrans = Convert.ToString(quantTrans);
                        lindv.EstadoTrans = objListLin.Valor("EstadoTrans");
                        double precoUnit = objListLin.Valor("PrecUnit");
                        precoUnit = Math.Round(precoUnit, 2);
                        lindv.PrecUnit = Convert.ToString(precoUnit);
                        double precoTotalLinha = objListLin.Valor("PrecoTotal");
                        lindv.PrecoTotalLinha = Convert.ToString(precoTotalLinha);

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }

            return listdv;
        }

        #endregion

        #region DocVendaPCK

        /* Função que devolve uma lista com todas as encomendas de uma certa série e com um certo estado */
        public static List<Model.DocVendaPCK> Encomendas_List_PCK(string tipoDoc, string serie, string estado, int picking)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVendaPCK dv = new Model.DocVendaPCK();
            List<Model.DocVendaPCK> listdv = new List<Model.DocVendaPCK>();
            Model.LinhaDocVendaPCK lindv = new Model.LinhaDocVendaPCK();
            List<Model.LinhaDocVendaPCK> listlindv = new List<Model.LinhaDocVendaPCK>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT Id, Data, Entidade, TipoDoc, NumDoc, DataCarga, HoraCarga, Serie, Documento, Estado FROM cabecdoc CD inner join CabecDocStatus ST ON CD.id = ST.IdCabecDoc");
                sql.Append(" WHERE TipoDoc='@1@'");
                sql.Append(" AND Serie='@2@'");
                sql.Append(" AND Estado='@3@'");

                if(picking == 1)
                    sql.Append(" AND Picking='@4@'");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);
                query = query.Replace("@2@", serie);
                query = query.Replace("@3@", estado);

                if(picking == 1)
                    query = query.Replace("@4@", picking.ToString());

                objListCab = PriEngine.Engine.Consulta(query);
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVendaPCK();
                    dv.Id = objListCab.Valor("Id");
                    DateTime dt = objListCab.Valor("Data");
                    dv.Data = dt.ToString("dd-MM-yyyy");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    int numDoc = objListCab.Valor("NumDoc");
                    dv.NumDoc = Convert.ToString(numDoc);
                    dv.DataCarga = objListCab.Valor("DataCarga");
                    dv.HoraCarga = objListCab.Valor("HoraCarga");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.Documento = objListCab.Valor("Documento");
                    dv.Estado = objListCab.Valor("Estado");
                    objListLin = PriEngine.Engine.Consulta("Select IdCabecDoc, NumLinha, Artigo, LD.Quantidade, Seccao, Armazem, Localizacao, Lote, Descricao, Id, Unidade, QuantTrans, EstadoTrans, PrecUnit,(PrecUnit * LD.Quantidade+ (PrecUnit*LD.Quantidade*(TaxaIva/100))) as PrecoTotal from LinhasDoc LD inner join LinhasDocStatus LDS ON LD.id = LDS.IdLinhasDoc AND IdCabecDoc='" + dv.Id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVendaPCK>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVendaPCK();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        int numLinha = objListLin.Valor("NumLinha");
                        lindv.NumLinha = Convert.ToString(numLinha);
                        lindv.Artigo = objListLin.Valor("Artigo");
                        double quantidade = objListLin.Valor("Quantidade");
                        lindv.Quantidade = Convert.ToString(quantidade);
                        lindv.Seccao = objListLin.Valor("Seccao");
                        lindv.Armazem = objListLin.Valor("Armazem");
                        lindv.Localizacao = objListLin.Valor("Localizacao");
                        lindv.Lote = objListLin.Valor("Lote");
                        lindv.Descricao = objListLin.Valor("Descricao");
                        lindv.Id = objListLin.Valor("Id");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        double quantTrans = objListLin.Valor("QuantTrans");
                        lindv.QuantTrans = Convert.ToString(quantTrans);
                        lindv.EstadoTrans = objListLin.Valor("EstadoTrans");
                        double precoUnit = objListLin.Valor("PrecUnit");
                        precoUnit = Math.Round(precoUnit, 2);
                        lindv.PrecUnit = Convert.ToString(precoUnit);
                        double precoTotalLinha = objListLin.Valor("PrecoTotal");
                        lindv.PrecoTotalLinha = Convert.ToString(precoTotalLinha);

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }

            return listdv;
        }

        /* Função que devolve uma lista com todas as encomendas do um certo id */
        public static List<Model.DocVendaPCK> Encomendas_List_PCK(string id)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVendaPCK dv = new Model.DocVendaPCK();
            List<Model.DocVendaPCK> listdv = new List<Model.DocVendaPCK>();
            Model.LinhaDocVendaPCK lindv = new Model.LinhaDocVendaPCK();
            List<Model.LinhaDocVendaPCK> listlindv = new List<Model.LinhaDocVendaPCK>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT Id, Data, Entidade, TipoDoc, NumDoc, DataCarga, HoraCarga, Serie, Documento, Estado FROM cabecdoc CD inner join CabecDocStatus ST ON CD.id = ST.IdCabecDoc");
                sql.Append(" WHERE Id='@1@'");
 
                query = sql.ToString();
                query = query.Replace("@1@", id);

                objListCab = PriEngine.Engine.Consulta(query);
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVendaPCK();
                    dv.Id = objListCab.Valor("Id");
                    DateTime dt = objListCab.Valor("Data");
                    dv.Data = dt.ToString("dd-MM-yyyy");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    int numDoc = objListCab.Valor("NumDoc");
                    dv.NumDoc = Convert.ToString(numDoc);
                    dv.DataCarga = objListCab.Valor("DataCarga");
                    dv.HoraCarga = objListCab.Valor("HoraCarga");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.Documento = objListCab.Valor("Documento");
                    dv.Estado = objListCab.Valor("Estado");
                    objListLin = PriEngine.Engine.Consulta("Select IdCabecDoc, NumLinha, Artigo, LD.Quantidade, Seccao, Armazem, Localizacao, Lote, Descricao, Id, Unidade, QuantTrans, EstadoTrans,PrecUnit,CONVERT(DECIMAL(10,2),(PrecUnit * LD.Quantidade+ (PrecUnit*LD.Quantidade*(TaxaIva/100)))) as PrecoTotalLinha from LinhasDoc LD inner join LinhasDocStatus LDS ON LD.id = LDS.IdLinhasDoc AND IdCabecDoc='" + dv.Id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVendaPCK>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVendaPCK();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        int numLinha = objListLin.Valor("NumLinha");
                        lindv.NumLinha = Convert.ToString(numLinha);
                        lindv.Artigo = objListLin.Valor("Artigo");
                        double quantidade = objListLin.Valor("Quantidade");
                        lindv.Quantidade = Convert.ToString(quantidade);
                        lindv.Seccao = objListLin.Valor("Seccao");
                        lindv.Armazem = objListLin.Valor("Armazem");
                        lindv.Localizacao = objListLin.Valor("Localizacao");
                        lindv.Lote = objListLin.Valor("Lote");
                        lindv.Descricao = objListLin.Valor("Descricao");
                        lindv.Id = objListLin.Valor("Id");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        double quantTrans = objListLin.Valor("QuantTrans");
                        lindv.QuantTrans = Convert.ToString(quantTrans);
                        lindv.EstadoTrans = objListLin.Valor("EstadoTrans");
                        lindv.PrecUnit = Convert.ToString(objListLin.Valor("PrecUnit"));
                        lindv.PrecoTotalLinha = Convert.ToString(objListLin.Valor("PrecoTotalLinha"));

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }

            return listdv;
        }

        public static string Encomendas_List_PCK_TotalPrice(string id)
        {
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVendaPCK dv = new Model.DocVendaPCK();
            List<Model.DocVendaPCK> listdv = new List<Model.DocVendaPCK>();
            Model.LinhaDocVendaPCK lindv = new Model.LinhaDocVendaPCK();
            List<Model.LinhaDocVendaPCK> listlindv = new List<Model.LinhaDocVendaPCK>();
            string precoTotal = "none";

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT Id, Data, Entidade, TipoDoc, NumDoc, DataCarga, HoraCarga, Serie, Documento, Estado FROM cabecdoc CD inner join CabecDocStatus ST ON CD.id = ST.IdCabecDoc");
                sql.Append(" WHERE Id='@1@'");

                query = sql.ToString();
                query = query.Replace("@1@", id);

                objListCab = PriEngine.Engine.Consulta(query);

                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVendaPCK();
                    dv.Id = objListCab.Valor("Id");
                    DateTime dt = objListCab.Valor("Data");
                    dv.Data = dt.ToString("dd-MM-yyyy");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.TipoDoc = objListCab.Valor("TipoDoc");
                    int numDoc = objListCab.Valor("NumDoc");
                    dv.NumDoc = Convert.ToString(numDoc);
                    dv.DataCarga = objListCab.Valor("DataCarga");
                    dv.HoraCarga = objListCab.Valor("HoraCarga");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.Documento = objListCab.Valor("Documento");
                    dv.Estado = objListCab.Valor("Estado");
                    objListLin = PriEngine.Engine.Consulta("select CONVERT(DECIMAL(10,2),Sum(PrecoTotalLinha)) AS PrecoTotal from (Select IdCabecDoc, NumLinha, Artigo, LD.Quantidade, Seccao, Armazem, Localizacao, Lote, Descricao, Id, Unidade, QuantTrans, EstadoTrans,CONVERT(DECIMAL(10,2),(PrecUnit * LD.Quantidade+ (PrecUnit*LD.Quantidade*(TaxaIva/100)))) as PrecoTotalLinha from LinhasDoc LD inner join LinhasDocStatus LDS ON LD.id = LDS.IdLinhasDoc AND IdCabecDoc='" + dv.Id + "') as t");

                    while (!objListLin.NoFim())
                    {
                        precoTotal = Convert.ToString(objListLin.Valor("PrecoTotal"));
                        objListLin.Seguinte();
                    }

                    objListCab.Seguinte();
                }
            }

            return precoTotal;
        }

        public static List<string> EncomendaSerie(string tipoDoc)
        {
            StdBELista objList;
            List<string> lis = new List<string>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT DISTINCT Serie from CabecDoc");
                sql.Append(" WHERE TipoDoc='@1@'");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);

                objList = PriEngine.Engine.Consulta(query);                

                while (!objList.NoFim())
                {
                    string temp = "";
                    temp = objList.Valor("Serie");
                    lis.Add(temp);
                    objList.Seguinte();
                }
            }

            return lis;
        }

        public static List<string> EncomendaSerie()
        {
            StdBELista objList;
            List<string> lis = new List<string>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT DISTINCT Serie from CabecDoc");

                query = sql.ToString();

                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    string temp = "";
                    temp = objList.Valor("Serie");
                    lis.Add(temp);
                    objList.Seguinte();
                }
            }

            return lis;
        }

        #endregion

        #region GuiaDeRemessa

        /* Função que cria uma nova guia de remessa */
        public static Model.RespostaErro GuiaDeRemessa_New(Model.CriaGuiaDeRemessa gr)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda ecl = new GcpBEDocumentoVenda();
            GcpBEDocumentoVenda new_gr = new GcpBEDocumentoVenda();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    ecl = PriEngine.Engine.Comercial.Vendas.Edita(gr.Filial, gr.TipoDoc, gr.Serie, gr.NumDoc);

                    PriEngine.Engine.IniciaTransaccao();
                    GcpBEDocumentoVenda[] a = new GcpBEDocumentoVenda[] { ecl };
                    new_gr.set_Serie(gr.Serie);
                    new_gr.set_Tipodoc("GR");
                    new_gr.set_TipoEntidade(gr.TipoEntidade);
                    new_gr.set_Entidade(gr.Entidade);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(new_gr);
                    Boolean dadosR = true;
                    PriEngine.Engine.Comercial.Vendas.TransformaDocumentoEX(a, new_gr, dadosR);
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";

                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 2;
                erro.Descricao = ex.Message;

                return erro;
            }
            //return erro;
        }

        #endregion

        #region Picking

        /* Função que verifica se uma transferência de um artigo é possível */
        public static Model.RespostaErro VerificaTransf(Model.TransItemPckArea artigo)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            List<Model.ArtigoArmazem> listartigos = new List<Model.ArtigoArmazem>();
            StdBELista objList;

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    StringBuilder sql = new StringBuilder();
                    string query = string.Empty;

                    sql.Append("select Artigo, Armazem, StkActual, Localizacao from ArtigoArmazem");
                    sql.Append(" WHERE Artigo='@1@'");
                    sql.Append(" AND Localizacao='@2@'");

                    query = sql.ToString();
                    query = query.Replace("@1@", artigo.Artigo.CodArtigo);
                    query = query.Replace("@2@", artigo.LocalizacaoDestino);
                    objList = PriEngine.Engine.Consulta(query);
                   
                    if (objList==null) {
                        erro.Erro = 1;
                        erro.Descricao = "Não existe a localização com esse artigo ou não existe esse artigo sequer";
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }

            }
            catch (Exception ex)
            {
                erro.Erro = 2;
                erro.Descricao = ex.Message;

                return erro;
            }

            return erro;
        }

        /* Função que transfere um item para a área de picking */
        public static Model.RespostaErro TransfereItemPickingArea(Model.TransItemPckArea artigo)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoStock documento = new GcpBEDocumentoStock();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    var data = DateTime.Now;
                    documento.set_Tipodoc(artigo.TipoDoc);
                    documento.set_Serie(artigo.Serie);

                    documento.set_ArmazemOrigem(artigo.ArmazemOrigem);
                    documento.set_DataDoc(data);

                    GcpBELinhasDocumentoStock lines = new GcpBELinhasDocumentoStock();
                    GcpBELinhasDocumentoStock item_lines = new GcpBELinhasDocumentoStock();
                    PriEngine.Engine.Comercial.Stocks.AdicionaLinha(documento, Artigo: artigo.Artigo.CodArtigo, Quantidade: artigo.Quantidade, Armazem: artigo.ArmazemDestino, Localizacao: artigo.LocalizacaoDestino);           
                    item_lines = documento.get_Linhas();

                    for (var i = 1; i <= item_lines.NumItens; ++i)
                    {
                        var line = item_lines.get_Edita(i);
                        line.set_LocalizacaoOrigem(artigo.LocalizacaoOrigem);
                        line.set_DataStock(data);
                        lines.Insere(line);
                    }

                    var avisos = String.Empty;
                    PriEngine.Engine.Comercial.Stocks.PreencheDadosRelacionados(documento);
                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Stocks.Actualiza(documento, ref avisos);
                    PriEngine.Engine.TerminaTransaccao();

                    erro.Descricao = avisos;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 2;
                erro.Descricao = ex.Message;

                return erro;
            }

            return erro;
        }

        public static List<string> TransferenciaSerie(string tipoDoc)
        {
            StdBELista objList;
            List<string> lis = new List<string>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("SELECT DISTINCT Serie from CabecStk");
                sql.Append(" WHERE TipoDoc='@1@'");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);

                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    string temp = "";
                    temp = objList.Valor("Serie");
                    lis.Add(temp);
                    objList.Seguinte();
                }
            }

            return lis;
        }

        public static IList<Model.TransferenciasObj> TransferenciaNumsSerie(string tipoDoc)
        {
            StdBELista objList;
            IList<Model.TransferenciasObj> lis = new List<Model.TransferenciasObj>();
            int temp = 0;

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("select Serie, count(NumDoc) as num from CabecStk");
                sql.Append(" where TipoDoc = '@1@' group by Serie");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);

                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    Model.TransferenciasObj obj = new Model.TransferenciasObj();
                    obj.Num = objList.Valor("num");
                    obj.Serie = objList.Valor("Serie");
                    lis.Add(obj);
                    objList.Seguinte();
                }
            }

            return lis;
        }

        public static IList<Model.LinhasStk> TransferenciasLinhas(string tipoDoc, string serie, int num)
        {
            StdBELista objList;
            IList<Model.LinhasStk> lis = new List<Model.LinhasStk>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StringBuilder sql = new StringBuilder();
                string query = string.Empty;

                //  TipoDoc='ECL' Serie='PCK' Estado=['P' | 'Q']
                sql.Append("select TipoDoc, NumDoc, NumLinha, Quantidade, Data, Armazem, Localizacao, LocalizacaoOrigem, Lote, EntradaSaida, Serie, Descricao from LinhasSTK");
                sql.Append("  where TipoDoc = '@1@' and Serie = '@2@' and NumDoc = '@3@' order by Quantidade, NumLinha desc");

                query = sql.ToString();
                query = query.Replace("@1@", tipoDoc);
                query = query.Replace("@2@", serie);
                query = query.Replace("@3@", num.ToString());

                objList = PriEngine.Engine.Consulta(query);

                while (!objList.NoFim())
                {
                    Model.LinhasStk obj = new Model.LinhasStk();
                    obj.TipoDoc = objList.Valor("TipoDoc");
                    obj.NumDoc = objList.Valor("NumDoc");
                    obj.NumLinha = objList.Valor("NumLinha");
                    obj.Quantidade = objList.Valor("Quantidade");
                    DateTime dt = objList.Valor("Data");
                    obj.Data = dt.ToString("dd-MM-yyyy");
                    obj.Armazem = objList.Valor("Armazem");
                    obj.Localizacao = objList.Valor("Localizacao");
                    obj.LocalizacaoOrigem = objList.Valor("LocalizacaoOrigem");
                    obj.Lote = objList.Valor("Lote");
                    obj.EntradaSaida = objList.Valor("EntradaSaida");
                    obj.Serie = objList.Valor("Serie");
                    obj.Descricao = objList.Valor("Descricao");
                    lis.Add(obj);
                    objList.Seguinte();
                }
            }

            return lis;
        }

        //public static Model.RespostaErro TransfereItemPickingArea(IList<Model.LinhaDocVendaPCK> )
        //{
        //    Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();

        //    return erro;
        //}
        public static Model.RespostaErro GeneratePickingList(int spacebin, string serie, int numencomendas)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            // List<Model.ArtigoArmazem> listartigos = new List<Model.ArtigoArmazem>();
            StdBELista objList;

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {


                    StringBuilder sql = new StringBuilder();
                    string query = string.Empty;

                    sql.Append("select LinhasDoc.Artigo,LinhasDoc.Localizacao,LinhasDoc.Quantidade,LinhasDoc.IdCabecDoc,LinhasDoc.Id,LinhasDoc.DataEntrega from LinhasDoc INNER JOIN CabecDoc ON CabecDoc.TipoDoc='ECL' AND LinhasDoc.IdCabecDoc=CabecDoc.Id AND LinhasDoc.Artigo!='NULL' AND CabecDoc.Serie='@5@' AND LinhasDoc.Localizacao!='NULL' order by LinhasDoc.DataEntrega ASC,LinhasDoc.IdCabecDoc DESC;");
                    //sql.Append(" WHERE Artigo='@1@'");
                    //sql.Append(" AND Localizacao='@2@'");



                    query = sql.ToString();
                    query = query.Replace("@5@", serie);
                    //query = query.Replace("@2@", artigo.LocalizacaoEntrada);
                    objList = PriEngine.Engine.Consulta(query);

                    if (objList == null)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "Não existe a localização com esse artigo ou não existe esse artigo sequer";
                        return erro;
                    }

                    List<Model.Encomenda> encomendas = new List<Model.Encomenda>();
                    Model.LinhaDaPickList art = new Model.LinhaDaPickList();
                    Model.Encomenda ecl = new Model.Encomenda();
                    int index = 0;

                    while (!objList.NoFim())
                    {

                        ecl.Id = objList.Valor("IdCabecDoc");
                        if (index == 0)
                            ecl.lista = new List<Model.LinhaDaPickList>();

                        DateTime datetempo = objList.Valor("DataEntrega");

                        ecl.Data = datetempo;


                        art.CodArtigo = objList.Valor("Artigo");

                        art.IdLinha = objList.Valor("Id");

                        art.Quantidade = objList.Valor("Quantidade");



                        art.Localizacao = objList.Valor("Localizacao");

                        ecl.lista.Add(art);

                        objList.Seguinte();

                        if (objList.NoFim())
                        {
                            encomendas.Add(ecl);
                        }
                        else
                        {
                            if (ecl.Id != objList.Valor("IdCabecDoc"))
                            {
                                encomendas.Add(ecl);
                                index = 0;
                            }
                            else
                            {
                                index = 1;
                            }
                        }

                    }
                    List<Model.Encomenda> pickList = new List<Model.Encomenda>();
                    /*  DateTime min = encomendas[0].Data;
                      int indexmin = 0;
                    
                      //selecionar as encomendas para sofrerem o processo de picking
                      for (int k = 0; k < numencomendas; k++)
                      {
                          for (int i = 0; i < encomendas.Count(); i++)
                          {


                              if (encomendas[i].Data < min && !pickList.Contains(encomendas[i]))
                              {
                                  min = encomendas[i].Data;
                                  indexmin = i;
                              }

                          }*/
                    for (int k = 0; k < numencomendas; k++)
                    {
                        pickList.Add(encomendas[k]);
                    }



                    StringBuilder sql1 = new StringBuilder();
                    string query1 = string.Empty;
                    int proxid;

                    StdBELista bomba = new StdBELista();
                    bomba = PriEngine.Engine.Consulta("select Max(Id) as proxid from PickingList");
                    StdBELista s = new StdBELista();
                    s = PriEngine.Engine.Consulta("select * from PickingList");
                    try
                    {
                        if (s.Vazia())
                        {
                            proxid = 0;
                            proxid++;
                        }
                        else
                        {
                            proxid = bomba.Valor("proxid");
                            proxid++;
                        }

                    }
                    catch (Exception e)
                    {
                        proxid = 0;
                        proxid++;
                    }

                    //Pegar no conjunto de encomendas selecionadas e criar as picking waves
                    //Falta ir procurar qual a localizacao mais proxima
                    for (int l = 0; l < pickList.Count(); l++)
                    {
                        for (int b = 0; b < pickList[l].lista.Count(); b++)
                        {
                            sql1 = new StringBuilder();
                            query1 = string.Empty;
                            sql1.Append("insert into PickingList (Id,Localizacao,Artigo,IdECL,Quantidade,EstadoTratado) values (@1@,'@2@','@3@','@4@',@5@,0)");
                            query1 = sql1.ToString();
                            query1 = query1.Replace("@1@", proxid.ToString());
                            query1 = query1.Replace("@2@", pickList[l].lista[b].Localizacao);
                            query1 = query1.Replace("@3@", pickList[l].lista[b].CodArtigo);

                            string[] deli = { "{", "}" };
                            string[] a = pickList[l].Id.Split(deli, System.StringSplitOptions.RemoveEmptyEntries);
                            int alphatest = a.Length;
                            query1 = query1.Replace("@4@", a[0]);





                            string nova = Convert.ToString(pickList[l].lista[b].Quantidade);

                            query1 = query1.Replace("@5@", nova);





                            // PriEngine.Engine.Consulta(query1);
                            System.Data.SqlClient.SqlConnection sqlConnection1 =
                        new System.Data.SqlClient.SqlConnection("Data Source=user-pc\\PRIMAVERA;Initial Catalog=PRIDEMOSINF;Integrated Security=True");

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = query1;
                            cmd.Connection = sqlConnection1;

                            sqlConnection1.Open();
                            cmd.ExecuteNonQuery();
                            sqlConnection1.Close();


                        }


                    }



                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {

                erro.Erro = 2;
                erro.Descricao = ex.Message;
                return erro;
            }

            return erro;
        }
        

        #endregion


    }
}
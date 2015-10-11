using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entity;
using DAL.Persistence;
using Site.Models;
using System.Data;
using Site.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace Site.Controllers
{
    [Authorize]
    public class UsuarioAutenticadoController : Controller
    {
        //
        // GET: /UsuarioAutenticado/


        public Double SomaTotalReceitas(int IdUsuario) 
        {
            Double Soma = 0;

            try
            {
                ReceitaDal dl = new ReceitaDal();

                List<Receita> list = dl.FindByUsuario(IdUsuario);
                
                foreach (Receita r in list)
                {
                    Soma = Soma + r.Valor; 
                }

                
            }
            catch (Exception ex)
            {
               ViewBag.Mensagem = ex.Message; 
            }

            return Soma;
        
        }

        public Double SomaTotalContasPagar(int IdUsuario)
        {
            Double Soma = 0;

            try
            {
                ContaPagarDal dl = new ContaPagarDal();

                List<ContaPagar> list = dl.FindByUsuario(IdUsuario);

                foreach (ContaPagar c in list)
                {
                    Soma = Soma + c.Valor;
                }


            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return Soma;

        }

        public ActionResult Home()
        {
            Usuario u = (Usuario)Session["usuario"];
            //imprimir...
            ViewBag.Usuario = u.Nome;
            ViewBag.TotalReceita = SomaTotalReceitas(u.IdUsuario);
            ViewBag.TotalContasPagar = SomaTotalContasPagar(u.IdUsuario);

            return View();
        }

        public ActionResult Receita()
        {

            return View("Receita");
        }

        [HttpPost]
        public ActionResult CadastrarReceita(ReceitaModelCadastro model)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                ReceitaDal rd = new ReceitaDal();
                
                if (   ModelState.IsValid
                    && u != null )
                {
                   Receita r = new Receita();

                   r.IdUsuario = u.IdUsuario;
                   r.Nome = model.Nome;
                   r.Valor = model.Valor;
                   r.DataRecebimento = model.DataRecebimento;

                   rd.Insert(r);

                   ViewBag.Mensagem = "A Receita " + model.Nome + ", foi cadastrada com sucesso !";

                   ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }           

            return View("Receita");
        }

        public ActionResult EditarReceita()
        {
            CarregarReceitas();

            return View("EditarReceita");
        }
        
        public void CarregarReceitas() 
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];

                if( u != null)
                {
                 ReceitaDal rd = new ReceitaDal();

                 ViewBag.Receitas = new SelectList(rd.FindByUsuario(u.IdUsuario), "IdReceita", "Nome");
                
                }


            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }
        }

        public ReceitaModelAtualizar SelecionarReceitas(int IdReceita) 
        {
            ReceitaModelAtualizar model = new ReceitaModelAtualizar();
            try
            {
                ReceitaDal rd = new ReceitaDal();

                Receita r = rd.FindById(IdReceita);

                if (r != null)
                {
                    model.IdReceita = r.IdReceita;
                    model.DataRecebimento = r.DataRecebimento;
                    model.Nome = r.Nome;
                    model.Valor = r.Valor;
                }
            }
            catch (Exception ex )
            {
                ViewBag.Mensagem = ex.Message;
            }

            return model;
        }


        public ActionResult SelecionarReceitasAtualiza()
        {
            int IdReceita = Convert.ToInt32(Request.Form["Receitas"]);
          
            CarregarReceitas();

            return View("EditarReceita", SelecionarReceitas(IdReceita));
        }

        public ActionResult SelecionarReceitasDeleta()
        {
            int IdReceita = Convert.ToInt32(Request.Form["Receitas"]);

            CarregarReceitas();

            return View("DeletarReceita", SelecionarReceitas(IdReceita));
        }

        [HttpPost]
        public ActionResult AtualizaReceita(ReceitaModelAtualizar model)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                ReceitaDal rd = new ReceitaDal();

                if (ModelState.IsValid
                    && u != null)
                {
                    Receita r = new Receita();
                    r.IdReceita = model.IdReceita;
                    r.Nome = model.Nome;
                    r.Valor = model.Valor;
                    r.DataRecebimento = model.DataRecebimento;
                    r.IdUsuario = u.IdUsuario;
                    rd.Update(r);

                    ViewBag.Mensagem = "A Receita " + model.Nome + ", foi atualizada com sucesso !";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }
            
            CarregarReceitas();

            return View("EditarReceita");
        }

        public ActionResult DeletarReceita()
        {
            CarregarReceitas();

            return View("DeletarReceita");
        }

        [HttpPost]
        public ActionResult DeletaReceita(ReceitaModelAtualizar model)
        {
            try
            {
                ReceitaDal rd = new ReceitaDal();

                if (ModelState.IsValid)
                {
                    rd.Delete(model.IdReceita);

                    ViewBag.Mensagem = "A Receita " + model.Nome + ", foi deletada com sucesso !";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }

            CarregarReceitas();

            return View("DeletarReceita");
        }
        
        public ActionResult BuscaReceita()
        {

            return View("BuscaReceita");
        }

        [HttpPost]
        public ActionResult GerarBuscaRelatorioReceita(FormCollection fomulario)
        {
            try
            {
                DateTime DataIncio = DateTime.MinValue;
                DateTime DataFim = DateTime.MinValue;
                Usuario u = (Usuario)Session["usuario"];

                if (!Request["DataIncio"].ToString().Equals(string.Empty))
                {
                    try
                    {
                        DataIncio = Convert.ToDateTime(Request["DataIncio"].ToString());
                    }
                    catch 
                    {
                        ViewBag.Mensagem = "Data Inicio Invalida! ";
                    }
                    
                }

                if (!Request["DataFim"].ToString().Equals(string.Empty))
                {
                    try
                    {
                        DataFim = Convert.ToDateTime(Request["DataFim"].ToString());
                    }
                    catch 
                    {
                        ViewBag.Mensagem = "Data Final Invalida! ";
                    }
                }

                if (DataIncio > DataFim) 
                {
                    throw new Exception("A Data de Incio não pode ser menor que a data Final.");
                }


                string formato = Request["formato"].ToString();
                 
                ReceitaDal d = new ReceitaDal();
                List<Receita> Lista = d.FindByRelatorio(DataIncio, DataFim, u.IdUsuario);
                DataSetReceita ds = new DataSetReceita();
                DataTable dt = ds.RECEITA;

                foreach (Receita r in Lista) 
                {
                    DataRow registro = dt.NewRow();
                    registro["CODIGO"] = r.IdReceita;
                    registro["NOME"] = r.Nome;
                    registro["DATARECEBIMENTO"] = r.DataRecebimento;
                    registro["VALOR"] = r.Valor;

                    dt.Rows.Add(registro);
                }

                RelReceita rel = new RelReceita();

                rel.SetDataSource(dt);

                Stream arquivo = null;

                switch (formato)
                {
                    case "1": //PDF
                        arquivo = rel.ExportToStream(ExportFormatType.PortableDocFormat);
                        return File(arquivo, "application/pdf", "relatorio.pdf");

                    case "2": //Word
                        arquivo = rel.ExportToStream(ExportFormatType.WordForWindows);
                        return File(arquivo, "application/msword", "relatorio");

                    case "3": //Excel
                        arquivo = rel.ExportToStream(ExportFormatType.Excel);
                        return File(arquivo, "application/excel", "relatorio");
                }
            }
            catch(Exception e)
            {
                
                ViewBag.Mensagem = e.Message;
            }

            return View("BuscaReceita");
        }

        public ActionResult ContaPagar()
        {

            return View("ContaPagar");
        }

        [HttpPost]
        public ActionResult CadastrarContaPagar(ContaPagarModelCadastro model)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                ContaPagarDal cd = new ContaPagarDal();

                if (ModelState.IsValid
                    && u != null)
                {
                    ContaPagar c = new ContaPagar();

                    c.IdUsuario = u.IdUsuario;
                    c.Nome = model.Nome;
                    c.Valor = model.Valor;
                    c.DataPagamento = model.DataPagamento;

                    cd.Insert(c);

                    ViewBag.Mensagem = "A Conta a Pagar " + model.Nome + ", foi cadastrada com sucesso !";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }

            return View("ContaPagar");
        }

        public ActionResult EditarContaPagar()
        {
            CarregarContaPagar();

            return View("EditarContaPagar");
        }

        public void CarregarContaPagar()
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];

                if (u != null)
                {
                    ContaPagarDal cd = new ContaPagarDal();

                    ViewBag.ContasPagar = new SelectList(cd.FindByUsuario(u.IdUsuario), "IdContaPagar", "Nome");

                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }
        }

        public ContaPagarModelAtualizar SelecionarContasPagar(int IdContaPagar)
        {
            ContaPagarModelAtualizar model = new ContaPagarModelAtualizar();
            try
            {
                ContaPagarDal cd = new ContaPagarDal();

                ContaPagar c = cd.FindById(IdContaPagar);

                if (c != null)
                {
                    model.IdContaPagar = c.IdContaPagar;
                    model.DataPagamento = c.DataPagamento;
                    model.Nome = c.Nome;
                    model.Valor = c.Valor;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return model;
        }

        public ActionResult SelecionarContaPagarAtualiza()
        {
            int IdContaPagar = Convert.ToInt32(Request.Form["ContasPagar"]);

            CarregarContaPagar();

            return View("EditarContaPagar", SelecionarContasPagar(IdContaPagar));
        }

        public ActionResult SelecionarContaPagarDeleta()
        {
            int IdContaPagar = Convert.ToInt32(Request.Form["ContasPagar"]);

            CarregarContaPagar();

            return View("DeletarContaPagar", SelecionarContasPagar(IdContaPagar));
        }

        [HttpPost]
        public ActionResult AtualizaContaPagar(ContaPagarModelAtualizar model)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                ContaPagarDal cd = new ContaPagarDal();

                if (ModelState.IsValid
                    && u != null)
                {
                    ContaPagar c = new ContaPagar();
                    c.IdContaPagar = model.IdContaPagar;
                    c.Nome = model.Nome;
                    c.Valor = model.Valor;
                    c.DataPagamento = model.DataPagamento;
                    c.IdUsuario = u.IdUsuario;
                    cd.Update(c);

                    ViewBag.Mensagem = "A Conta a Pagar " + model.Nome + ", foi atualizada com sucesso !";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }

            CarregarContaPagar();

            return View("EditarContaPagar");
        }

        public ActionResult DeletarContaPagar()
        {
            CarregarContaPagar();

            return View("DeletarContaPagar");
        }

        [HttpPost]
        public ActionResult DeletaContaPagar(ContaPagarModelAtualizar model)
        {
            try
            {
                ContaPagarDal cd = new ContaPagarDal();

                if (ModelState.IsValid)
                {
                    cd.Delete(model.IdContaPagar);

                    ViewBag.Mensagem = "A Conta a Pagar " + model.Nome + ", foi deletada com sucesso !";

                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {

                ViewBag.Mensagem = ex.Message;
            }

            CarregarContaPagar();

            return View("DeletarContaPagar");
        }

        public ActionResult BuscaContaPagar()
        {

            return View("BuscaContaPagar");
        }

        [HttpPost]
        public ActionResult GerarBuscaRelatorioContaPagar(FormCollection fomulario)
        {
            try
            {
                DateTime DataIncio = DateTime.MinValue;
                DateTime DataFim = DateTime.MinValue;
                Usuario u = (Usuario)Session["usuario"];

                if (!Request["DataIncio"].ToString().Equals(string.Empty))
                {
                    try
                    {
                        DataIncio = Convert.ToDateTime(Request["DataIncio"].ToString());
                    }
                    catch
                    {
                        ViewBag.Mensagem = "Data Inicio Invalida! ";
                    }

                }

                if (!Request["DataFim"].ToString().Equals(string.Empty))
                {
                    try
                    {
                        DataFim = Convert.ToDateTime(Request["DataFim"].ToString());
                    }
                    catch
                    {
                        ViewBag.Mensagem = "Data Final Invalida! ";
                    }
                }

                if (DataIncio > DataFim)
                {
                    throw new Exception("A Data de Incio não pode ser menor que a data Final.");
                }


                string formato = Request["formato"].ToString();

                ContaPagarDal d = new ContaPagarDal();
                List<ContaPagar> Lista = d.FindByRelatorio(DataIncio, DataFim, u.IdUsuario);
                DataSetContaPagar ds = new DataSetContaPagar();
                DataTable dt = ds.CONTAPAGAR;

                foreach (ContaPagar c in Lista)
                {
                    DataRow registro = dt.NewRow();
                    registro["CODIGO"] = c.IdContaPagar;
                    registro["NOME"] = c.Nome;
                    registro["DATAPAGAMENTO"] = c.DataPagamento;
                    registro["VALOR"] = c.Valor;

                    dt.Rows.Add(registro);
                }

                RelContaPagar rel = new RelContaPagar();

                rel.SetDataSource(dt);

                Stream arquivo = null;

                switch (formato)
                {
                    case "1": //PDF
                        arquivo = rel.ExportToStream(ExportFormatType.PortableDocFormat);
                        return File(arquivo, "application/pdf", "relatorio.pdf");

                    case "2": //Word
                        arquivo = rel.ExportToStream(ExportFormatType.WordForWindows);
                        return File(arquivo, "application/msword", "relatorio");

                    case "3": //Excel
                        arquivo = rel.ExportToStream(ExportFormatType.Excel);
                        return File(arquivo, "application/excel", "relatorio");
                }
            }
            catch (Exception e)
            {

                ViewBag.Mensagem = e.Message;
            }

            return View("BuscaContaPagar");
        }
    }
}

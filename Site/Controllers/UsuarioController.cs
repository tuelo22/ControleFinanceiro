using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using DAL.Entity;
using DAL.Persistence;
using DAL.Util;
using System.Web.Security;

namespace Site.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioModelCadastro model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    if (model.Senha.Equals(model.SenhaConfirm)) 
                    {
                        UsuarioDal d = new UsuarioDal();

                        if(! d.HasLogin(model.Login))
                        {
                            Usuario u = new Usuario();
                            u.Nome = model.Nome;
                            u.Login = model.Login;
                            u.Senha =  Criptografia.EncriptarMD5(model.Senha);

                            d.Inserir(u);

                            ViewBag.Mensagem = "Usuário "+ u.Nome + ", cadastrado com sucesso.";

                            ModelState.Clear();
                        }
                        else
                        {
                            throw new Exception("Erro. Login indisponivel. Tente outro");
                        }
                       
                    }
                    else
                    {
                        throw new Exception("Erro. Informe as senhas corretamente.");
                    }
                } 
            }
            catch (Exception ex)
            {
                
                 ViewBag.Mensagem = ex.Message;
            }
            
            return View("Cadastro");
        }


        [HttpPost]
        public ActionResult AutenticarUsuario(UsuarioModelLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioDal d = new UsuarioDal();
                    Usuario u = d.Find(model.Login, Criptografia.EncriptarMD5(model.Senha));

                    if (u != null)
                    {
                        FormsAuthentication.SetAuthCookie(u.Login, false);

                        Session.Add("usuario", u);

                        return RedirectToAction("Home", "UsuarioAutenticado");

                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso Negado. Tente Novamente.";
                    }
                }
                

            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Remove("usuario");
            Session.Abandon();

            return View("Login");
        }


    }
}

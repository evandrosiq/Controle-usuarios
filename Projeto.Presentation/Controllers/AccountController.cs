using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Presentation.Models;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.CrossCutting.Criptography;
using Projeto.CrossCutting.Mail;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace Projeto.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioRepository usuariorepository;
        private readonly IPerfilRepository perfilRepository;
        private readonly MD5Encrypt mD5Encrypt;
        private readonly MailService mailService;

        public AccountController(IUsuarioRepository usuariorepository, 
                                 IPerfilRepository perfilRepository, 
                                 MD5Encrypt mD5Encrypt, MailService mailService)
        {
            this.perfilRepository = perfilRepository;
            this.usuariorepository = usuariorepository;
            this.mD5Encrypt = mD5Encrypt;
            this.mailService = mailService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AutenticarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = usuariorepository.Find
                        (model.Email, mD5Encrypt.GenerateHash(model.Senha));

                    if (usuario != null)
                    {
                        var identity = new ClaimsIdentity(new[] 
                        {
                            new Claim(ClaimTypes.Name, usuario.Email),

                            new Claim(ClaimTypes.Role, usuario.Perfil.Nome)
                        },
                        CookieAuthenticationDefaults.AuthenticationScheme);

                        var autenticacao = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync
                            (CookieAuthenticationDefaults
                            .AuthenticationScheme, autenticacao);
                                                                     
                        return RedirectToAction("Index", "AreaRestrita");
                    } else
                    {
                        TempData["Mensagem"] = "Acesso negado. Usuário inválido.";
                    }
                }
                catch (Exception ex)
                {

                    TempData["Mensagem"] = "Ocorreu um erro: " + ex.Message;
                }

            }

            return View();
        }

        public IActionResult Register()
        {
            return View(GerarUsuarioModel());
        }

        [HttpPost]
        public IActionResult Register(CriarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (usuariorepository.Find(model.Email) != null)
                    {
                        TempData["Mensagem"] = "Esta email já está cadastrado.";
                    } else
                    {
                        var usuario = new Usuario();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = mD5Encrypt.GenerateHash(model.Senha);
                        usuario.DataCriacao = DateTime.Now;
                        usuario.Status = 1;
                        usuario.IdPerfil = model.IdPerfil;


                        usuariorepository.Create(usuario);

                        TempData["Mensagem"] = "Usuário criado com sucesso.";
                        ModelState.Clear();

                        EnviarEmailDeBoasvindas(usuario);
                    }                

                }
                catch (Exception ex)
                {

                    TempData["Mensagem"] = ex.Message;
                }
            }
            return View(GerarUsuarioModel());
        }

        private void EnviarEmailDeBoasvindas(Usuario usuario)
        {
            var assunto = "Conta criada com sucesso - CARNAVAL";

            var texto = new StringBuilder();

            texto.Append($"Olá, {usuario.Nome}\n\n");
            texto.Append($"Sua conta foi criada com sucesso!\n");
            texto.Append($"Faça seu login para acessar ao sistema.");
            texto.Append($"\n\n");
            texto.Append($"Evandro Siqueira - COTI Informática");

            mailService.SendMail(usuario.Email, assunto, texto.ToString());
        }

        private CriarUsuarioModel GerarUsuarioModel()
        {
            var model = new CriarUsuarioModel();
            model.ListagemPerfis = new List<SelectListItem>();

            foreach (var item in perfilRepository.FindAll())
            {
                var opcao = new SelectListItem();
                opcao.Value = item.IdPerfil.ToString();
                opcao.Text = item.Nome;

                model.ListagemPerfis.Add(opcao);
            }
            return model;
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
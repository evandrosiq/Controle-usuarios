using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;

namespace Projeto.Presentation.Controllers
{
    [Authorize]
    public class AreaRestritaController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;

        public AreaRestritaController(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MinhaConta()
        {
            Usuario usuario = null;

            try
            {
                usuario = usuarioRepository.Find(User.Identity.Name);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
            }

            return View(usuario);
        }

        public IActionResult Usuarios()
        {
            var usuarios = new List<Usuario>();

            try
            {
                usuarios = usuarioRepository.FindAll();
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
            }

            return View(usuarios);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Presentation.Controllers
{
    [Authorize]
    public class AreaRestritaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
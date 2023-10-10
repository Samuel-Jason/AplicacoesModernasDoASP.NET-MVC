using Aplicacoes.Data;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacoes.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;

        public IActionResult Index()
        {
            return View();
        }
    }
}

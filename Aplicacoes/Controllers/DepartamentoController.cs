using Aplicacoes.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplicacoes.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;

        public DepartamentoController(IESContext context)
        {
            this._context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await  _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
        }
    }
}

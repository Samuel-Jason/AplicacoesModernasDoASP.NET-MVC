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
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await  _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome")] DepartamentoController departamento)
        {
            try
            {
                if (ModelState.IsValid) //MODELSTATE VERIFICA ANTES DO OBJETO SER ADICIONADO SE NAO TEM ERRO DE VALIDACAO E ENTAO GRAVA
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException) 
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados");
            }
            return View(departamento);
        }
    }
}

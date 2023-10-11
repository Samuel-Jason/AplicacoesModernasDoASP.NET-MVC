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
            return View(await _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
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

        [HttpPost]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound(); //retorna HTTPNOTFOUND COMO SE FOSSE UM HTTP 404
            }

            var departamento = _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            await _context.SaveChangesAsync();

            //    if(!= DepartamentoExists(DepartamentoController.DepartamentoID)) VERIFICA SE EXITE NA BASE ALGUM OBJ COM ID RECEBIDO
            //        return NotFound()


            if (departamento == null)
            {
                return NotFound(nameof(departamento));
            }
            return View(departamento);
        }

        private bool DepartamentoExistis(long? id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoId == id);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId.Equals(id));

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        [HttpGet]
        public async Task<IActionResult> Delete (long? id)
        {
            var departamento = _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId.Equals(id));
            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Aplicacoes.Data;
using Aplicacoes.Models;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Modelo.Cadastro
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly InstituicaoDAL instituicaoDAL;

        public DepartamentoController(IESContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos
                .Include(i => i.Instituicao)
                .OrderBy(c => c.Nome)
                .ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome, InstituicaoId")] DepartamentoModel departamento)
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

        public async Task<IActionResult> Create([Bind("Nome, InstituicaoID")] DepartamentoModel departamento)
        {
            var instituicoes = _context.Instituicoes
                .OrderBy(i => i.Nome)
                .ToList();
            instituicoes.Insert(0, new InstituicaoModel()
            {
                InstituicaoId = 0,
                Nome = "Selecione	a	instituição"
            });
            ViewBag.Instituicoes = instituicoes;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Edit(long? id, [Bind("DepartamentoID, Nome, InstituicaoID")] DepartamentoModel departamentos)
        {
            if (id != null)
            {
                var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);

                ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoId);

                return View(departamento);
            }

            return NotFound(); //retorna HTTPNOTFOUND COMO SE FOSSE UM HTTP 404
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

            var departamento = await _context.Instituicoes
                .Include(d => d.Departamentos)
                .SingleOrDefaultAsync(m => m.InstituicaoId == id);
                 _context.Instituicoes.Where(i => departamento.InstituicaoId == i.InstituicaoId).Load();

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        //	POST:	Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            _context.Departamentos.Remove(departamento);
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            var departamento = await _context.Departamentos
                .SingleOrDefaultAsync(m => m.DepartamentoId == id);
            _context.Instituicoes.Where(i => departamento.InstituicaoId == i.InstituicaoId).Load();

            if (departamento == null)
            {
                return NotFound("Error");
            }
            return View(departamento);
        }


    }
}

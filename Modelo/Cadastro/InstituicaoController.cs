using Aplicacoes.Data;
using Aplicacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Modelo.Cadastro;
using System.Linq;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aplicacoes.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;

        public InstituicaoController(IESContext context) 
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
        }

        private static IList<InstituicaoModel> instituicoes = new List<InstituicaoModel>()
        {
            new InstituicaoModel()
            {
                InstituicaoId = 1,
                Nome = "UniParana",
                Endereco = "Parana"
            },
            new InstituicaoModel()
            {
                InstituicaoId = 2,
                Nome = "Unisanta",
                Endereco = "santa catarina"
            },
            new InstituicaoModel()
            {
                InstituicaoId = 3,
                Nome = "UniSaoPaulo",
                Endereco = "SaoPaulo"
            },
            new InstituicaoModel()
            {
                InstituicaoId = 4,
                Nome = "UniSulgrandence",
                Endereco = "Rio grande do sul"
            },
            new InstituicaoModel()
            {
                InstituicaoId = 5,
                Nome = "UniCarioca",
                Endereco = "Rio de janeiro"
            }
        };

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Endereco")] InstituicaoModel instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await instituicaoDAL.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados");
            }
            return View(instituicao);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(long? id, [Bind("InstituicaoID, Nome, Endereco")] InstituicaoModel instituicao)
        {
            if (id == null || instituicao.InstituicaoId != id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await instituicaoDAL.GravarInstituicao(instituicao);
                }
                catch (DbUpdateException)
                {
                    // Correção: Removida a vírgula após a condição do if
                    if (!await InstituicaoModel.Exists(instituicao.InstituicaoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Index()
        {
            return View(await instituicaoDAL.ObterInstituicoesClassificadasPorNome().ToListAsync());
        }

        public IActionResult Create(InstituicaoModel instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.InstituicaoId = instituicoes.Select(i => i.InstituicaoId).Max() + 1;
            return RedirectToAction("Index");
        }

        public IActionResult Edit(InstituicaoModel instituicao, int id)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoId == id).FirstOrDefault());
            instituicoes.Add(instituicao);
            return RedirectToAction("Index");
        }

        public async ActionResult Details(long id)
        {
            return await ObterVisaoInstituicaoId(id);
        }
            //return View(instituicoes.Where(i => i.InstituicaoId == id).FirstOrDefault());

        public IActionResult Delete(int id)
        {
            var instituicaoExcluir = instituicoes.FirstOrDefault(i => i.InstituicaoId == id);

            try
            {
                if (instituicaoExcluir != null)
                {
                    instituicoes.Remove(instituicaoExcluir);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("NotFound");
            }

        }

        public async Task<IActionResult> ObterVisaoInstituicaoId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await instituicaoDAL.ObterInstituicaoId((long) id);

            if(instituicao == null)
            {
                return NotFound();
            }
            
            return View(instituicao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await instituicaoDAL.EliminarInstituicaoId((long) id);
            TempData["Message"] = "Instituicao" + instituicao.Nome.ToUpper() + "foi removida");
            return RedirectToAction(nameof(Index));
        }
    }
}

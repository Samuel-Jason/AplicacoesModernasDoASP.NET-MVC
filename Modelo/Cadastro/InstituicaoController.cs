using Aplicacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Modelo.Cadastro;

namespace Aplicacoes.Controllers
{
    public class InstituicaoController : Controller
    {


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

        public async Task<IActionResult> Index()
        {
            return View(await InstituicaoDAL.ObterInstituicoesClassificadasPorNome().ToListAsync());
        }

        public IActionResult Create(InstituicaoModel instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.InstituicaoId =
                instituicoes.Select(i => i.InstituicaoId).Max() + 1;
            return RedirectToAction("Index");
        }

        public IActionResult Edit(InstituicaoModel instituicao, int id)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoId == id).FirstOrDefault());
            instituicoes.Add(instituicao);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoId == id).FirstOrDefault());
        }

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
    }
}

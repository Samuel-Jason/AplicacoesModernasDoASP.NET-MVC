using Aplicacoes.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View(instituicoes);
        }

        public IActionResult Create(InstituicaoModel instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.InstituicaoId =
                instituicoes.Select(i => i.InstituicaoId).Max() + 1;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(InstituicaoModel instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoId == instituicao.InstituicaoId)
                .FirstOrDefault());
            instituicoes.Add(instituicao);
            return RedirectToAction("Index");
        }
    }
}

using Aplicacoes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacoes.Controllers
{
    public class InstituicaoController : Controller
    {
        private static IList<Instituicao> instituicoes = new List<Instituicao>()
        {
            new Instituicao()
            {
                InstituicaoId = 1,
                Nome = "UniParana",
                Endereco = "Parana"
            },
            new Instituicao()
            {
                InstituicaoId = 2,
                Nome = "Unisanta",
                Endereco = "santa catarina"
            },
            new Instituicao()
            {
                InstituicaoId = 3,
                Nome = "UniSaoPaulo",
                Endereco = "SaoPaulo"
            },
            new Instituicao()
            {
                InstituicaoId = 4,
                Nome = "UniSulgrandence",
                Endereco = "Rio grande do sul"
            },
            new Instituicao()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }
    }
}

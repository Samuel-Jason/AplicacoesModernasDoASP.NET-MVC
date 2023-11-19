using Aplicacoes.Data;
using Aplicacoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cadastro
{
    public class InstituicaoDAL
    {
        private readonly IESContext _context;

        public InstituicaoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<InstituicaoModel> ObterInstituicoesClassificadasPorNome()
        {
            return _context.Instituicoes.OrderBy(m => m.Nome);
        }

    }
}

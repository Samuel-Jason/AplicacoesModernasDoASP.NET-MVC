using Aplicacoes.Data;
using Aplicacoes.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<InstituicaoModel> ObterInstituicaoId(long id)
        {
            return await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoId == id);
        }

        public async Task<InstituicaoModel> GravarInstituicao(InstituicaoModel instituicao)
        {
            if(instituicao.InstituicaoId == null)
            {
                _context.Instituicoes.Add(instituicao);
            }
            else
            {
                _context.Update(instituicao);
            }

            await _context.SaveChangesAsync();
            return instituicao;
        }

        public async Task<InstituicaoModel> EliminarInstituicaoId(long id)
        {
            InstituicaoModel instituicao = await ObterInstituicaoId(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        } 



    }
}

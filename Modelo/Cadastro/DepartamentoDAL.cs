using Aplicacoes.Data;
using Aplicacoes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastro
{
    public class DepartamentoDAL
    {
        private readonly IESContext _context;

        public DepartamentoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<DepartamentoController> ObterDepartamentosClassificadosPorNome()
        {
            return (IQueryable<DepartamentoController>)_context.Departamentos.Include(i => i.Instituicao).OrderBy(b => b.Nome);
        }

        public async Task<DepartamentoController> ObterDepartamentoPorId(long id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoId == id);

            if (departamento != null)
            {
                _context.Instituicoes.Where(i => departamento.InstituicaoId == i.InstituicaoId).Load();
            }

            return departamento;
        }

        public async Task<DepartamentoController> GravarDepartamento(DepartamentoModel departamento)
        {
            if (departamento.DepartamentoId == 0) // Alterado de null para 0
            {
                _context.Departamentos.Add(departamento);
            }
            else
            {
                _context.Update(departamento);
            }
            await _context.SaveChangesAsync();
            return departamento;
        }

        public async Task<DepartamentoController> EliminarDepartamentoPorId(long id)
        {
            var departamento = await ObterDepartamentoPorId(id);

            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();
            }

            return departamento;
        }
    }
}
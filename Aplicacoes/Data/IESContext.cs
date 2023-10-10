using Aplicacoes.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplicacoes.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<DbContext> options) : base(options) 
        {
        }

        public DbSet<DepartamentoModel> Departamentos { get; set; }
        public DbSet<InstituicaoModel> Instituicoes { get; set; }

    }
}

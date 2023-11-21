using Aplicacoes.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplicacoes.Data
{
        public class IESContext : DbContext
        {
            public IESContext(DbContextOptions<IESContext> options) : base(options)
            {
            }

            public DbSet<DepartamentoModel> Departamentos { get; set; }
            public DbSet<InstituicaoModel> Instituicoes { get; set; }
            public DbSet<Curso> Cursos { get; set; }
            public DbSet<Disciplina> Disciplinas { get; set; }
            public DbSet<Academico> Academicos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<CursoDisciplina>()
                    .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });

                modelBuilder.Entity<CursoDisciplina>()
                    .HasOne(cd => cd.Curso)
                    .WithMany(c => c.CursosDisciplinas)
                    .HasForeignKey(cd => cd.CursoID);

                modelBuilder.Entity<CursoDisciplina>()
                    .HasOne(cd => cd.Disciplina)
                    .WithMany(d => d.CursosDisciplinas)
                    .HasForeignKey(cd => cd.DisciplinaID);
            }
        }
}

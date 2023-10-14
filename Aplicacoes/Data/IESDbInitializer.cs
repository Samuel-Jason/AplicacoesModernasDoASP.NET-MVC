using Aplicacoes.Models;

namespace Aplicacoes.Data
{
    public static class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var instituicoes = new InstituicaoModel[]
            {
                new InstituicaoModel {Nome="UniParaná", Endereco = "Paraná"},
                new InstituicaoModel {Nome="UniAcre", Endereco = "Acre"}
            };

            foreach (InstituicaoModel i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }
            context.SaveChanges();

            var departamentos = new DepartamentoModel[]
            {
                new DepartamentoModel { Nome="Ciencia da computacao", InstituicaoId = 1},
                new DepartamentoModel { Nome="Ciencia de alimentos", InstituicaoId = 2}
            };

            foreach (DepartamentoModel d in departamentos)
            {
                context.Departamentos.Add(d);
            }
            context.SaveChanges();

        }
    }
}

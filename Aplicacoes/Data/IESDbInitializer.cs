using Aplicacoes.Models;

namespace Aplicacoes.Data
{
    public static class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureCreated();

            if(context.Departamentos.Any())
            {
                return;
            }

            var departamentos = new DepartamentoModel[]
            {
                new DepartamentoModel { Nome="Ciencia da computacao"},
                new DepartamentoModel { Nome="Ciencia de alimentos"}
            };

            foreach(DepartamentoModel d in departamentos)
            {
                context.Departamentos.Add(d);
            }
            context.SaveChanges();

        }
    }
}

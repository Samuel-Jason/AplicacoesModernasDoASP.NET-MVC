namespace Aplicacoes.Models
{
    public class DepartamentoModel
    {
        public long? DepartamentoId { get; set; }
        public string Nome { get; set; }

        public long? InstituicaoId { get; set; }
        public InstituicaoModel Instituicao { get; set; }
    }
}

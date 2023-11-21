using Aplicacoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Curso 
    {
        public long? CursoID {  get; set; }
        public string Nome { get; set; }
        public long? DepartamentoID { get; set;}
        public DepartamentoModel Departamento { get; set; }
        public virtual ICollection<CursoDisciplina> CursoDisciplina { get; set; }
    }
}

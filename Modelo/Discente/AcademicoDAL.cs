using Aplicacoes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Discente
{
    public class AcademicoDAL
    {
        private readonly IESContext _context;
        public AcademicoDAL(IESContext context)
        {
            _context = context;
        }
    }
}

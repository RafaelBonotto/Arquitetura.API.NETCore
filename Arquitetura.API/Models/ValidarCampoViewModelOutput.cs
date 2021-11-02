using System.Collections.Generic;

namespace Arquitetura.API.Models
{
    public class ValidarCampoViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidarCampoViewModelOutput(IEnumerable<string> _erros)
        {
            Erros = _erros;
        }
    }
}

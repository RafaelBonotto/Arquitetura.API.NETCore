using Arquitetura.API.Models;

namespace Arquitetura.API.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuario);
    }
}

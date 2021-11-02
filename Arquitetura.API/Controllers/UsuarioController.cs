using Arquitetura.API.Filters;
using Arquitetura.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Arquitetura.API.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Esse serviço permite autenticar um usuario cadastrado e ativo.
        /// Params: Login, Senha
        /// </summary>
        /// <param name="loginViewModelInput">View Model do Login</param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type =typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type =typeof(ValidarCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type =typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {            
            return Ok(loginViewModelInput);
        }

        /// <summary>
        /// Registrar Usuario no sitema.
        /// Params: Login, Email, Senha
        /// </summary>
        /// <param name="registrarViewModelInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Regsitrar(RegistrarViewModelInput registrarViewModelInput)
        {
            return Created("", registrarViewModelInput);
        }
    }
}

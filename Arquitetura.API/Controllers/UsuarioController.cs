using Arquitetura.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arquitetura.API.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Fazer Login no sistema
        /// Params: Login, Senha
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("logar")]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            return Ok(loginViewModelInput);
        }

        /// <summary>
        /// Registrar Usuario no sitema
        /// Params: Login, Email, Senha
        /// </summary>
        /// <param name="registrarViewModelInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrar")]
        public IActionResult Regsitrar(RegistrarViewModelInput registrarViewModelInput)
        {
            return Created("", registrarViewModelInput);
        }
    }
}

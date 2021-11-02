using Arquitetura.API.Filters;
using Arquitetura.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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
            //Usuario Moock:
            var usuarioMoock = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "nomedousuario",
                Email = "emaildousuario@email.com"
            };

            // GERANDO O JWT
            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jH\\2.}8ZP'qY#7");// PEGAR DO appsettings
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioMoock.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioMoock.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioMoock.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerate = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerate);


            return Ok(new
            {
                Token = token,
                Usuario = usuarioMoock
            });
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

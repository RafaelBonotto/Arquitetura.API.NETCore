using Arquitetura.API.Business.Entities;
using Arquitetura.API.Filters;
using Arquitetura.API.Infraestruture.Data;
using Arquitetura.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /// Esse serviço permite cadastrar um usuario no sitema.
        /// </summary>
        /// <param name="registrarViewModelInput">View model de registro de login</param>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type =typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Regsitrar(RegistrarViewModelInput registrarViewModelInput)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            var migrationsPendentes = contexto.Database.GetPendingMigrations();
            if (migrationsPendentes.Count() > 0)
            {
                contexto.Database.Migrate();
            }

            var usuario = new Usuario();
            usuario.Login = registrarViewModelInput.Login;
            usuario.Email = registrarViewModelInput.Email;
            usuario.Senha = registrarViewModelInput.Senha;
            contexto.Usuario.Add(usuario);
            contexto.SaveChanges();


            return Created("", registrarViewModelInput);
        }
    }
}

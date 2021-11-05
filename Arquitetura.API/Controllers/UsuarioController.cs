using Arquitetura.API.Business.Entities;
using Arquitetura.API.Business.Repositorys;
using Arquitetura.API.Configurations;
using Arquitetura.API.Filters;
using Arquitetura.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Arquitetura.API.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authentication;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration, IAuthenticationService authentication)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _authentication = authentication;
        }

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
            Usuario usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if (usuario is null)
                return BadRequest("Usuário não encontrado no sistema");

            //if (usuario.Senha != loginViewModel.Senha.GerarSenhaCriptografada())
            //    return BadRequest("Erro ao tentar acessar");

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {     
                Codigo = usuario.Codigo,
                Login = usuario.Login,
                Email = usuario.Email
            };            

            var token = _authentication.GerarToken(usuarioViewModelOutput);  

            return Ok(new
            {
                Token = token,
                Usuario = usuario
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
            var usuario = new Usuario()
            {
                Login = registrarViewModelInput.Login,
                Email = registrarViewModelInput.Email,
                Senha = registrarViewModelInput.Senha
            };
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.commit();
            
            return Created("", usuario);
        }
    }
}

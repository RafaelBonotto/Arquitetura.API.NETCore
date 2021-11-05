using Arquitetura.API.Business.Entities;
using Arquitetura.API.Business.Repositorys;
using Arquitetura.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Arquitetura.API.Controllers
{
    [Route("api/v1/curso")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoReppository;

        public CursoController(ICursoRepository repository)
        {
            _cursoReppository = repository;
        }

        /// <summary>
        /// Este serviço permite cadastrar curso para usuario autenticado
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Retorna status 201 e dados do curso do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar o curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostAsync(CursoViewModelInput cursoViewModelInput)
        {
            //Codigo(Id) do usuario logado:
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Curso curso = new Curso()
            {
                Nome = cursoViewModelInput.Nome,
                Descricao = cursoViewModelInput.Descricao,
                CodigoUsuario = codigoUsuario
            };
            _cursoReppository.Adicionar(curso);
            _cursoReppository.Commit();
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os curso ativos do usuario
        /// </summary>
        /// <returns>Retorna status ok e dados do curso do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _cursoReppository.ObterCursosPorUsuario(codigoUsuario)
                .Select(x => new CursoViewModelOutput
                {
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    Login = x.Usuario.Login
                });

            if (cursos is null)
                return NotFound("Nenhum curso encontrado para esse usuário");

            return Ok(cursos);
        }
    }
}

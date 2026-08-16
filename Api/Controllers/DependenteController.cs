using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DependenteController : ControllerBase
    {
        private readonly DependenteService _dependenteService;

        public DependenteController(DependenteService dependenteService)
        {
            _dependenteService = dependenteService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] DependenteCadastroDto dto)
        {
            try
            {
                var idLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(idLogado))
                {
                    return Unauthorized(new { erro = "Usuário não autenticado." });
                }

                await _dependenteService.CadastrarAsync(Guid.Parse(idLogado), dto);
                return StatusCode(201, new { mensagem = "Dependente cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var idLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idLogado)) return Unauthorized();

            var dependentes = await _dependenteService.ObterPorAssinanteAsync(Guid.Parse(idLogado));
            return Ok(dependentes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] DependenteCadastroDto dto)
        {
            try
            {
                var idLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(idLogado)) return Unauthorized();

                await _dependenteService.AtualizarAsync(id, Guid.Parse(idLogado), dto);
                return Ok(new { mensagem = "Dependente atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var idLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(idLogado)) return Unauthorized();

                await _dependenteService.DeletarAsync(id, Guid.Parse(idLogado));
                return Ok(new { mensagem = "Dependente removido com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
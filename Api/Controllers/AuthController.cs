using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroDto dto)
        {
            try
            {
                await _usuarioService.CadastrarAsync(dto);
                return StatusCode(201, new { mensagem = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            try
            {
                //chama o service
                var token = await _usuarioService.LoginAsync(dto);

                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { erro = ex.Message });
            }
        }

        [HttpGet("perfil")]
        [Authorize]
        public IActionResult MeuPerfil()
        {
            // pro autocomplete de editarPerfil
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var nome = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new
            {
                Mensagem = "Acesso autorizado, dados:",
                UsuarioId = id,
                Nome = nome,
                Email = email
            });
        }

        [HttpPut("perfil")]
        [Authorize]
        public async Task<IActionResult> EditarPerfil([FromBody] UsuarioEdicaoDto dto)
        {
            try
            {
                //endpoint p editar o perfil

                // pega o id do token, masi seguro assim
                var idLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                //chama o service com id e o payload
                if (idLogado != null)
                {
                    await _usuarioService.AtualizarPerfilAsync(Guid.Parse(idLogado), dto);
                }

                return Ok(new { mensagem = "Perfil atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
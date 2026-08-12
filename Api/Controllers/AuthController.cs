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
        [Authorize] //tipo o bagulho de security do spring
        public IActionResult MeuPerfil()
        {
            //extraindo os dados do token (vi q essa parada é diferente do spring, a  ideia no .NET é extrair do token, de alguma forma que não compreendi completamente)
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
    }
}
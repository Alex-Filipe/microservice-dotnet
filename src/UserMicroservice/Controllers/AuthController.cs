using Microsoft.AspNetCore.Mvc;
using UserMicroserice.Dtos.AuthDtos;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController(AuthService service) : ControllerBase
    {
        private readonly AuthService _authService = service;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto autenticacao)
        {
            try
            {
                var autenticar = _authService.Authenticate(autenticacao);
                return Ok(autenticar);
            }
            catch (Exception e)
            {
                var errorResponse = new { Message = $"Erro: {e.Message}" };
                return StatusCode(500, errorResponse);
            }
        }
    }
}

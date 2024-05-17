using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userService = userServices;

        [HttpPost("new_user")]
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                _userService.CreateUser(user);
                
                return Ok(new { Message = "Usuário criado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}

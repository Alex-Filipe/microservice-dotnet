using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Dtos;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController(UserService userServices) : ControllerBase
    {
        private readonly UserService _userService = userServices;

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

        [HttpPut("update_user/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto userUpdateRequest)
        {
            try
            {
                userUpdateRequest.Id = id; 
                _userService.UpdateUser(userUpdateRequest);
                return Ok(new { Message = "Usuário atualizado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}

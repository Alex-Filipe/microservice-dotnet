using Microsoft.AspNetCore.Mvc;
using UserMicroserice.Dtos.UserDTOs;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController(UserService userServices) : ControllerBase
    {
        private readonly UserService _userService = userServices;

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

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

        [HttpGet("view_user/{id}")]
        public IActionResult ShowUser(int id)
        {
            try
            {
                var user = _userService.ShowUser(id);
                return Ok(user);
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

        [HttpDelete("delete_user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(new { Message = "Usuário excluído com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}

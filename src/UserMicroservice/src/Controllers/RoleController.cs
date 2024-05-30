using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.src.Services;
using UserMicroserice.src.Dtos.RoleDTOs;


namespace UserMicroservice.src.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api")]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;

        [HttpGet("roles/{user_id}")]
        public IActionResult GetAllRoles(int user_id)
        {
            try
            {
                var roles = _roleService.GetAllRoles(user_id);
                return Ok(roles);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPost("new_role")]
        public IActionResult CreateRole([FromBody] CreateRoleDto role)
        {
            try
            {
                _roleService.CreateRole(role);
                return Ok(new { Message = "Perfíl criado com sucesso." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpGet("view_role/{id}")]
        public IActionResult ShowRole(int id)
        {
            try
            {
                var role = _roleService.ShowRole(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }

        [HttpPut("update_role/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateRoleDto updatedRole)
        {
            try
            {
                updatedRole.Id = id; 
                _roleService.UpdateRole(updatedRole);
                return Ok(new { Message = "Perfíl atualizado com sucesso" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = $"Erro: {e.Message}" });
            }
        }
    }
}

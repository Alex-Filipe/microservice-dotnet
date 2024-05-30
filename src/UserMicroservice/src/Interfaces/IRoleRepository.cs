using UserMicroserice.src.Dtos.RoleDTOs;
using UserMicroservice.src.Models;

namespace UserMicroservice.src.Interfaces
{
    public interface IRoleRepository
    {
        List<AllRolesDto> GetAllRoles(int user_id);
        void CreateRole(CreateRoleDto newRole);
        void UpdateRole(UpdateRoleDto updatedRole);
        Role? GetRoleByName(string roleName);
        Role? GetRoleById(int roleId);
    }
}
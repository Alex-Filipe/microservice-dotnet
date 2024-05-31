using UserMicroserice.src.Dtos.RoleDTOs;
using UserMicroservice.src.Interfaces;
using UserMicroservice.src.Models;

namespace UserMicroservice.src.Services
{
    public class RoleService(IRoleRepository roleRepository)
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public List<AllRolesDto> GetAllRoles(int user_id)
        {
            try
            {
                return _roleRepository.GetAllRoles(user_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateRole(CreateRoleDto role)
        {
            try
            {
                ValidateRoleNameUniqueness(role.Name);
                ValidateUserRoleLimit(role.User_id);

                CreateRoleDto newRole = new()
                {
                    Name = role.Name,
                    User_id = role.User_id
                };

                _roleRepository.CreateRole(newRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Role ShowRole(int roleId)
        {
            try
            {
                var role = _roleRepository.GetRoleById(roleId) ?? throw new Exception("Perfil não encontrado.");
                return role;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateRole(UpdateRoleDto updatedRole)
        {
            try
            {
                _roleRepository.UpdateRole(updatedRole);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ValidateRoleNameUniqueness(string roleName)
        {
            if (_roleRepository.GetRoleByName(roleName) != null)
            {
                throw new Exception("Já existe um perfil com este nome.");
            }
        }

        private void ValidateUserRoleLimit(int userId)
        {
            if (_roleRepository.GetAllRoles(userId).Count >= 4)
            {
                throw new Exception("O usuário já possui 4 perfis.");
            }
        }
    }
}
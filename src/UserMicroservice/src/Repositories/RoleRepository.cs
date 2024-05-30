using UserMicroserice.src.Dtos.RoleDTOs;
using UserMicroservice.src.Database;
using UserMicroservice.src.Interfaces;
using UserMicroservice.src.Models;

namespace UserMicroservice.src.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Role? GetRoleByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(role => role.Name == roleName);
        }

        public Role? GetRoleById(int roleId)
        {
            return _context.Roles.FirstOrDefault(role => role.Id == roleId);
        }

        public List<AllRolesDto> GetAllRoles(int user_id)
        {
            try
            {
                return [.. _context.Roles
                               .Where(role => role.User_id == user_id)
                               .Select(role => new AllRolesDto
                               {
                                   Id = role.Id,
                                   Name = role.Name,
                                   User_id = role.User_id
                               })
                               .OrderBy(u => u.Id)];
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer todos os perfis. Contate o suporte!", e);
            }
        }


        public void CreateRole(CreateRoleDto newRole)
        {
            var role = new Role
            {
                Name = newRole.Name,
                User_id = newRole.User_id
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(UpdateRoleDto updatedRole)
        {
            var role = _context.Roles.FirstOrDefault(role => role.Id == updatedRole.Id) ?? throw new Exception("Perfil não encontrado.");
            role.Name = updatedRole.Name;

            _context.SaveChanges();
        }
    }
}
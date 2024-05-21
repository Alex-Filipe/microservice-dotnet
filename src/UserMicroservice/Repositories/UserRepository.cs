

using UserMicroservice.Database;
using UserMicroservice.Dtos;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public void CreateUser(CreateUserDto newUser)
        {
            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                Phone = newUser.Phone
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(UpdateUserDto updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id) ?? throw new ArgumentException("Usuário não encontrado");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            
            _context.SaveChanges();
        }
    }
}
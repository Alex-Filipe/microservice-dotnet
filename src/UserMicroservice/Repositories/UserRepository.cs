

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
    }
}
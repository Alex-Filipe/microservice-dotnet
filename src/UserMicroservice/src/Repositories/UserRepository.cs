using UserMicroserice.src.Dtos.UserDTOs;
using UserMicroservice.src.Database;
using UserMicroservice.src.Interfaces;
using UserMicroservice.src.Models;

namespace UserMicroservice.src.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public User? GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(user => user.Email == email);
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer o usuários. Contate o suporte!", e);
            }
        }

        public bool IsEmailInUse(string email, int userId)
        {
            return _context.Users.Any(u => u.Email == email && u.Id != userId);
        }

        public User? GetUserById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(user => user.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer o usuários. Contate o suporte!", e);
            }
        }

        public List<AllUserDto> GetAllUsers()
        {
            try
            {
                return [.. _context.Users
                               .Select(user => new AllUserDto
                               {
                                   Id = user.Id,
                                   Name = user.Name,
                                   Email = user.Email,
                                   DateBirth = user.DateBirth
                               })
                               .OrderBy(u => u.Name)];
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer todos os usuários. Contate o suporte!", e);
            }
        }

        public void CreateUser(CreateUserDto newUser)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = new User
                {
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    DateBirth = newUser.DateBirth
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception("Erro ao criar usuário no banco. Contate o suporte!", e);
            }
        }

        public ShowUserDto? ShowUser(int id)
        {
            try
            {
                return _context.Users
                               .Where(user => user.Id == id)
                               .Select(user => new ShowUserDto
                               {
                                   Id = user.Id,
                                   Name = user.Name,
                                   Email = user.Email,
                                   DateBirth = user.DateBirth
                               })
                               .FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer o usuário. Contate o suporte!", e);
            }
        }

        public void UpdateUser(User existingUser, UpdateUserDto updatedUser)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                existingUser.DateBirth = updatedUser.DateBirth;

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new ApplicationException("Erro ao atualizar usuário no banco. Contate o suporte!", e);
            }
        }

        public void DeleteUser(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = GetUserById(id) ?? throw new Exception("Erro ao encontrar o usuário!");

                _context.Users.Remove(user);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new ApplicationException("Erro ao deletar usuário. Contate o suporte!", e);
            }
        }

    }
}

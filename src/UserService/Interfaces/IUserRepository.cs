using UserService.Dtos;
using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void CreateUser(CreateUserDto newUser);

    }
}
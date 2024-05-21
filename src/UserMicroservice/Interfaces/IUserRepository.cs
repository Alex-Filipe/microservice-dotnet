
using UserMicroservice.Dtos;
using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void CreateUser(CreateUserDto newUser);
    }
}
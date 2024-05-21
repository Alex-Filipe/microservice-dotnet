
using UserMicroservice.Dtos;
using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        User? GetUserById(int id);
        void CreateUser(CreateUserDto newUser);
        void UpdateUser(UpdateUserDto updatedUser);
    }
}
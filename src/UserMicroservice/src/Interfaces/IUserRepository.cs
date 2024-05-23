using UserMicroserice.src.Dtos.UserDTOs;
using UserMicroservice.src.Models;

namespace UserMicroservice.src.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        bool IsEmailInUse(string email, int userId);
        User? GetUserById(int id);
        ShowUserDto? ShowUser(int id);
        List<AllUserDto> GetAllUsers();
        void CreateUser(CreateUserDto newUser);
        void UpdateUser(User existingUser, UpdateUserDto updatedUser);
        void DeleteUser(int id);
    }
}
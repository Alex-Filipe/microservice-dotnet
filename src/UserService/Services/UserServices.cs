using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Dtos;
using UserService.Interfaces;

namespace UserService.Services
{
    public class UserServices(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public void CreateUser(CreateUserDto user)
        {
            try
            {
                if (_userRepository.GetUserByEmail(user.Email) != null)
                {
                    throw new ArgumentException("Já existe um usuário com esse e-mail.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                CreateUserDto newUser = new()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = hashedPassword,
                    Phone = user.Phone
                };

                _userRepository.CreateUser(newUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
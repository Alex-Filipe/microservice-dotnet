using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Producers;
using UserMicroservice.Dtos;
using UserMicroservice.Interfaces;
using UserMicroserice.Dtos;

namespace UserMicroservice.Services
{
    public class UserService(IUserRepository userRepository, UserProducer userProducer)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserProducer _rabbitMQClient = userProducer;

        public List<AllUserDto> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
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
                _rabbitMQClient.SendMessageUserToQueue("user_email_queue", user.Email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateUser(UpdateUserDto updatedUser)
        {
            try
            {
                var existingUserWithId = _userRepository.GetUserById(updatedUser.Id) ?? throw new ArgumentException("O email já está sendo usado por outro usuário.");
                
                _userRepository.UpdateUser(updatedUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
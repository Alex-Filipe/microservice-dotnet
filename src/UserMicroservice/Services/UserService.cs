using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Producers;
using UserMicroservice.Dtos;
using UserMicroservice.Interfaces;

namespace UserMicroservice.Services
{
    public class UserService(IUserRepository userRepository, UserProducer userProducer)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserProducer _rabbitMQClient = userProducer;
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
    }
}
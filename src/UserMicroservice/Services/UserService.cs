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
                var existingUser = _userRepository.GetUserById(updatedUser.Id) ?? throw new Exception("Usuário não existe.");

                bool emailInUse = _userRepository.GetUserByEmail(updatedUser.Email) != null && existingUser.Email != updatedUser.Email;
                if (emailInUse)
                {
                    throw new Exception("Já existe um usuário com esse e-mail.");
                }

                _userRepository.UpdateUser(existingUser, updatedUser);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar usuário. Contate o suporte!", e);
            }
        }

    }
}
using UserMicroservice.src.Producers;
using UserMicroservice.src.Interfaces;
using UserMicroserice.src.Dtos.UserDTOs;


namespace UserMicroservice.src.Services
{
    public class UserService(IUserRepository userRepository, UserProducer userProducer)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserProducer _userProducer = userProducer;

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
                    DateBirth = user.DateBirth
                };

                _userRepository.CreateUser(newUser);
                _userProducer.SendMessageUserToQueue("user_email_queue", user.Email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ShowUserDto ShowUser(int roleId)
        {
            try
            {
                var user = _userRepository.ShowUser(roleId)  ?? throw new Exception("Usuário não encontrado.");
                return user;
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

                if (_userRepository.IsEmailInUse(updatedUser.Email, updatedUser.Id))
                {
                    throw new Exception("Já existe um usuário com este email.");
                }

                _userRepository.UpdateUser(existingUser, updatedUser);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar usuário. Contate o suporte!", e);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var existingUser = _userRepository.GetUserById(id) ?? throw new Exception("Usuário não existe.");

                _userRepository.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
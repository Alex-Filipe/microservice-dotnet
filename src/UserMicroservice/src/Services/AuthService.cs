using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserMicroserice.src.Dtos.AuthDtos;
using UserMicroservice.src.Interfaces;

namespace UserMicroservice.src.Services
{
    public class AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly IUserRepository _userRepository = userRepository;

        public object Authenticate(LoginDto autenticacao)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(autenticacao.Email);

                if (user == null || !VerifyPassword(autenticacao.Password, user.Password))
                {
                    throw new ArgumentException("Credenciais inválidas");
                }

                // Gere o token JWT se o login for bem-sucedido
                var token = GenerateJwtToken(user.Email);
                var tokenExpire = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).AddHours(1);

                return new
                {
                    access_token = token,
                    expires_in = tokenExpire.ToString("dd/MM/yyyy HH:mm:ss"),
                    user = new
                    {
                        user_id = user.Id,
                        name = user.Name,
                        email = user.Email,
                        date_birth = user.DateBirth
                    }
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        private static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        private string GenerateJwtToken(string username)
        {
            try
            {
                var secretKey = _configuration["Jwt:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new ArgumentException("A chave secreta do JWT não foi configurada corretamente.");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
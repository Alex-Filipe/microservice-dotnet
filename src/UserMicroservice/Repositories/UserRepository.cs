using System;
using Microsoft.EntityFrameworkCore;
using UserMicroserice.Dtos;
using UserMicroservice.Database;
using UserMicroservice.Dtos;
using UserMicroservice.Interfaces;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public User? GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(user => user.Email == email);
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer o usuários. Contate o suporte!", e);
            }
        }

        public bool IsEmailInUse(string email, int userId)
        {
            return _context.Users.Any(u => u.Email == email && u.Id != userId);
        }

        public User? GetUserById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(user => user.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer o usuários. Contate o suporte!", e);
            }
        }

        public List<AllUserDto> GetAllUsers()
        {
            try
            {
                return [.. _context.Users
                               .Select(user => new AllUserDto
                               {
                                   Id = user.Id,
                                   Name = user.Name,
                                   Email = user.Email,
                                   Phone = user.Phone
                               })
                               .OrderBy(u => u.Name)];  // Correção do método
            }
            catch (Exception e)
            {
                throw new Exception("Um erro ocorreu ao trazer todos os usuários. Contate o suporte!", e);
            }
        }

        public void CreateUser(CreateUserDto newUser)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = new User
                {
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    Phone = newUser.Phone
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception("Erro ao criar usuário no banco. Contate o suporte!", e);
            }
        }

        public void UpdateUser(User existingUser, UpdateUserDto updatedUser)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                existingUser.Phone = updatedUser.Phone;

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new ApplicationException("Erro ao atualizar usuário no banco. Contate o suporte!", e);
            }
        }

    }
}

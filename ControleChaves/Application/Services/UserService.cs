using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;

namespace ControleChaves.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ControleChavesContext _context;

        public UserService(ControleChavesContext context) => _context = context;
        

        public Task<(bool, Usuario)> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = _context.Usuarios.ToList().Find(u => u.Email == username && u.Senha == password);
            var result = (user != null, user);

            return Task.FromResult(result);
        }
    }
}

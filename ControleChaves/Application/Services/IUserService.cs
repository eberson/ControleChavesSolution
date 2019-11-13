using ControleChaves.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public interface IUserService
    {
        Task<(bool, Usuario)> ValidateUserCredentialsAsync(string username, string password);
    }
}

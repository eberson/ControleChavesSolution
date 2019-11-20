using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public interface IUsuarioService
    {
        Task Save(UsuarioViewModel vm);

        Task Remove(int id);

        Task<List<UsuarioViewModel>> FindAll();

        Task<UsuarioViewModel> Find(int id);
    }
}

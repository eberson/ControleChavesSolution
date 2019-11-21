using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using ControleChaves.Models;

namespace ControleChaves.Application.Services
{
    public class UsuarioService : BaseService<Usuario, UsuarioViewModel, int>, IUsuarioService
    {
        public UsuarioService(ControleChavesContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override Usuario Search(UsuarioViewModel vm)
        {
            return _db.Find(vm.ID);
        }

        protected override void Update(Usuario element, UsuarioViewModel vm)
        {
            element.Nome = vm.Nome;
            element.Email = vm.Email;
            element.Status = Status.ATIVO;
        }
    }
}

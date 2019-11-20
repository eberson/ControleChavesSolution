using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using ControleChaves.Models;

namespace ControleChaves.Application.Services
{
    public class FuncionarioService : BaseService<Funcionario, FuncionarioViewModel, int>, IFuncionarioService
    {
        public FuncionarioService(ControleChavesContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override Funcionario Search(FuncionarioViewModel vm)
        {
            return _db.Find(vm.ID);
        }

        protected override void Update(Funcionario element, FuncionarioViewModel vm)
        {
            element.Email = vm.Email;
            element.Celular = vm.Celular;
            element.DataNascimento = vm.DataNascimento;
            element.Funcao = vm.Funcao;
            element.Nome = vm.Nome;
        }
    }
}

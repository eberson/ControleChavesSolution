using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public interface IControleService
    {
        Task<ControleViewModel> Lend(EmprestimoViewModel vm);

        Task<ControleViewModel> GiveBack(DevolucaoViewModel vm);

        Task Remove(int id);

        Task WarnEmployee(int controleID);

        Task<ControleViewModel> Find(int id);

        Task<List<ControleViewModel>> FindAll();
        
        Task<List<ControleViewModel>> FindAll(DateTime when);

        Task<List<ControleViewModel>> FindAll(ChaveViewModel vm);

        Task<List<ControleViewModel>> FindAll(FuncionarioViewModel vm);
    }
}

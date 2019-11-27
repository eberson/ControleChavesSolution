using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public interface IChaveService : IBaseService<ChaveViewModel, string>
    {
        Task<List<ChaveViewModel>> FindAll(int localizacaoID);

        Task<List<ChaveMovimentacaoViewModel>> FindAvailable();
    }
}

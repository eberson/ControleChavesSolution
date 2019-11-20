using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public interface IBaseService<VM, K>
    {
        Task Create(VM vm);

        Task Update(VM vm);

        Task Remove(K id);

        Task<List<VM>> FindAll();

        Task<VM> Find(K id);
    }
}

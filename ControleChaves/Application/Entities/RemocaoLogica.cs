using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Entities
{
    public interface RemocaoLogica
    {
        void MarkAsRemoved();

        bool IsActive();
    }
}

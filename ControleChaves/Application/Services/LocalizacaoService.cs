using AutoMapper;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public class LocalizacaoService : BaseService<Localizacao, LocalizacaoViewModel, int>, ILocalizacaoService
    {
        public LocalizacaoService(ControleChavesContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override Localizacao Search(LocalizacaoViewModel vm)
        {
            return _db.Find(vm.ID);
        }

        protected override void Update(Localizacao element, LocalizacaoViewModel vm)
        {
            element.Descricao = vm.Descricao;
            element.Chaves.ToList().RemoveAll(c => !vm.Chaves.Any(vmc => vmc.Numero == c.Numero));

            vm.Chaves.Where(vmc => !element.Chaves.Any(c => c.Numero == vmc.Numero))
                .ToList()
                .ForEach(vmc => element.Chaves.Add(_mapper.Map<Chave>(vmc)));
        }
    }
}

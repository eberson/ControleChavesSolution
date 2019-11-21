using AutoMapper;
using AutoMapper.QueryableExtensions;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using ControleChaves.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public class ChaveService : BaseService<Chave, ChaveViewModel, string>, IChaveService
    {
        public ChaveService(ControleChavesContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<List<ChaveViewModel>> FindAll(int localizacaoID)
        {
            var result = _db.Include(e => e.Localizacao)
               .AsEnumerable()
               .Where(e => e.IsActive() && e.Localizacao.ID == localizacaoID)
               .AsQueryable()
               .ProjectTo<ChaveViewModel>(_mapper.ConfigurationProvider)
               .ToList();

            return Task.FromResult(result);
        }

        protected override Chave Search(ChaveViewModel vm)
        {
            return _db.Find(vm.Numero);
        }

        protected override void Update(Chave element, ChaveViewModel vm)
        {
            element.Status = Status.ATIVO;
        }

        public override Task<ChaveViewModel> Find(string id)
        {
            return Task.FromResult(_db.Include(c => c.Localizacao).Where(c => c.Numero == id).ProjectTo<ChaveViewModel>(_mapper.ConfigurationProvider).FirstOrDefault());
        }

        public override Task<ChaveViewModel> Create(ChaveViewModel vm)
        {
            var e = Search(vm);

            if (e != null)
            {
                return Update(vm);
            }

            e = _mapper.Map<Chave>(vm);

            e.Localizacao = _context.Localizacoes.Find(vm.LocalizacaoID);

            _db.Add(e);
            _context.SaveChanges();

            return Task.FromResult(_mapper.Map<ChaveViewModel>(e));
        }

        public override Task<ChaveViewModel> Update(ChaveViewModel vm)
        {
            var element = Search(vm);

            element.Localizacao = _context.Localizacoes.Find(vm.LocalizacaoID);

            Update(element, vm);

            _db.Update(element);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<ChaveViewModel>(element));
        }
    }
}

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
            element.Status = Status.ATIVO;
        }

        public override Task Remove(int id)
        {
            try
            {
                var element = _db.Include(l => l.Chaves).Where(l => l.ID == id).FirstOrDefault();

                element.Chaves.ToList().ForEach(c => Remove(c));
                Remove(element);

                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        private void Remove<E>(E e) where E: class
        {
            if (e is RemocaoLogica)
            {
                ((RemocaoLogica)e).MarkAsRemoved();
            }
            else
            {
                _context.Entry<E>(e).State = EntityState.Deleted;
            }
        }

        public override Task<List<LocalizacaoViewModel>> FindAll()
        {
            var result = _db.Include(e => e.Chaves)
               .AsEnumerable()
               .Where(e => e.IsActive())
               .AsQueryable()
               .ProjectTo<LocalizacaoViewModel>(_mapper.ConfigurationProvider)
               .ToList();

            return Task.FromResult(result);
        }

        public override Task<LocalizacaoViewModel> Find(int id)
        {
            var result = _db.Include(l => l.Chaves)
                .Where(l => l.ID == id)
                .ProjectTo<LocalizacaoViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            return Task.FromResult(result);
        }
    }
}

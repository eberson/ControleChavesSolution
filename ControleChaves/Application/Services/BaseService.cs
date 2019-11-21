using AutoMapper;
using AutoMapper.QueryableExtensions;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public abstract class BaseService<E, VM, K> : IBaseService<VM, K> where E : class
    {
        protected readonly ControleChavesContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<E> _db;

        public BaseService(ControleChavesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _db = _context.Set<E>();
        }

        public virtual Task<VM> Find(K id)
        {
            return Task.FromResult(_mapper.Map<VM>(_db.Find(id)));
        }

        protected bool ShouldAvoid(E element)
        {
            if (element is RemocaoLogica)
            {
                return !((RemocaoLogica)element).IsActive();
            }

            return false;
        }

        public virtual Task<List<VM>> FindAll()
        {
            var result = _db.AsEnumerable()
                .Where(e => e is RemocaoLogica ? ((RemocaoLogica)e).IsActive() : true)
                .AsQueryable()
                .ProjectTo<VM>(_mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(result);
        }

        public virtual Task Remove(K id)
        {
            try
            {
                var element = _db.Find(id);

                if (element is RemocaoLogica)
                {
                    ((RemocaoLogica)element).MarkAsRemoved();
                }
                else
                {
                    _db.Remove(element);
                }

                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public virtual Task<VM> Create(VM vm)
        {
            var e = Search(vm);

            if (e != null)
            {
                return Update(vm);
            }

            e = _mapper.Map<E>(vm);
            _db.Add(e);




            _context.SaveChanges();

            return Task.FromResult(_mapper.Map<VM>(e));
        }

        public virtual Task<VM> Update(VM vm)
        {
            var element = Search(vm);

            Update(element, vm);

            _db.Update(element);
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<VM>(element));
        }

        protected abstract void Update(E element, VM vm);

        protected abstract E Search(VM vm);
    }
}

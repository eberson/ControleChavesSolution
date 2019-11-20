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

        //private readonly ControleChavesContext _context;
        //private readonly IMapper _mapper;

        //public UsuarioService(ControleChavesContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        //public Task<UsuarioViewModel> Find(int id)
        //{
        //    return Task.FromResult(_mapper.Map<UsuarioViewModel>(_context.Usuarios.Find(id)));
        //}

        //public Task<List<UsuarioViewModel>> FindAll()
        //{
        //    var result = _context.Usuarios.ProjectTo<UsuarioViewModel>(_mapper.ConfigurationProvider).ToList();
        //    return Task.FromResult(result);
        //}

        //public Task Remove(int id)
        //{
        //    try
        //    {
        //        var usuario = _context.Usuarios.Find(id);

        //        _context.Usuarios.Remove(usuario);
        //        _context.SaveChanges();
        //        return Task.CompletedTask;
        //    }
        //    catch (Exception e)
        //    {
        //        return Task.FromException(e);
        //    }
        //}

        //public Task Save(UsuarioViewModel vm)
        //{
        //    try
        //    {
        //        var usuario = _context.Usuarios.Find(vm.ID);

        //        usuario.Email = vm.Email;
        //        usuario.Nome = vm.Nome;

        //        if (vm.ID > 0)
        //        {
        //            _context.Usuarios.Update(usuario);
        //        } else
        //        {
        //            _context.Usuarios.Add(usuario);
        //        }

        //        _context.SaveChanges();
        //        return Task.CompletedTask;
        //    }
        //    catch (Exception e)
        //    {
        //        return Task.FromException(e);
        //    }

        //}
        protected override Usuario Search(UsuarioViewModel vm)
        {
            return _db.Find(vm.ID);
        }

        protected override void Update(Usuario element, UsuarioViewModel vm)
        {
            element.Nome = vm.Nome;
            element.Email = vm.Email;
        }
    }
}

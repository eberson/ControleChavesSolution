using AutoMapper;
using AutoMapper.QueryableExtensions;
using ControleChaves.Application.Database;
using ControleChaves.Application.Entities;
using ControleChaves.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControleChaves.Application.Services
{
    public class ControleService : IControleService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ControleChavesContext _context;
        private readonly IMapper _mapper;

        public ControleService(ControleChavesContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        private Usuario CurrentUser
        {
            get
            {
                var id = int.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                return _context.Usuarios.Find(id);
            }
        } 

        public Task<ControleViewModel> Find(int id)
        {
            var result = _context.Controles
                .Include(c => c.FuncionarioDevolucao)
                .Include(c => c.FuncionarioRetirada)
                .Include(c => c.UsuarioDevolucao)
                .Include(c => c.UsuarioRetirada)
                .Include(c => c.Chave)
                .Where(c => c.Codigo == id)
                .ProjectTo<ControleViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            return Task.FromResult(result);
        }

        public Task<List<ControleViewModel>> FindAll()
        {
            var result = _context.Controles
                .Include(c => c.FuncionarioDevolucao)
                .Include(c => c.FuncionarioRetirada)
                .Include(c => c.UsuarioDevolucao)
                .Include(c => c.UsuarioRetirada)
                .Include(c => c.Chave)
                .ProjectTo<ControleViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<List<ControleViewModel>> FindAll(ChaveViewModel vm)
        {
            var result = _context.Controles
                .Include(c => c.FuncionarioDevolucao)
                .Include(c => c.FuncionarioRetirada)
                .Include(c => c.UsuarioDevolucao)
                .Include(c => c.UsuarioRetirada)
                .Include(c => c.Chave)
                .Where(c => c.Chave.Numero == vm.Numero)
                .ProjectTo<ControleViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<List<ControleViewModel>> FindAll(FuncionarioViewModel vm)
        {
            var result = _context.Controles
                .Include(c => c.FuncionarioDevolucao)
                .Include(c => c.FuncionarioRetirada)
                .Include(c => c.UsuarioDevolucao)
                .Include(c => c.UsuarioRetirada)
                .Include(c => c.Chave)
                .Where(c => c.FuncionarioRetirada.ID == vm.ID)
                .ProjectTo<ControleViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<List<ControleViewModel>> FindAll(DateTime when)
        {
            var result = _context.Controles
                .Include(c => c.FuncionarioDevolucao)
                .Include(c => c.FuncionarioRetirada)
                .Include(c => c.UsuarioDevolucao)
                .Include(c => c.UsuarioRetirada)
                .Include(c => c.Chave)
                .Where(c => c.Retirada.Date == when.Date && c.Devolucao == null)
                .ProjectTo<ControleViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<ControleViewModel> GiveBack(DevolucaoViewModel vm)
        {
            var controle = _context.Controles.Find(vm.Codigo);

            controle.FuncionarioDevolucao = _mapper.Map<Funcionario>(vm.Funcionario);
            controle.UsuarioDevolucao = CurrentUser;
            controle.Devolucao = vm.Data;

            _context.Controles.Update(controle);
            _context.SaveChanges();

            return Task.FromResult(_mapper.Map<ControleViewModel>(controle));
        }

        public Task<ControleViewModel> Lend(EmprestimoViewModel vm)
        {
            var controle = _mapper.Map<Controle>(vm);

            controle.UsuarioRetirada = CurrentUser;

            _context.Controles.Add(controle);
            _context.SaveChanges();

            return Task.FromResult(_mapper.Map<ControleViewModel>(controle));
        }

        public Task Remove(int id)
        {
            try
            {
                var element = _context.Controles.Find(id);

                _context.Controles.Remove(element);
                _context.SaveChanges();

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task WarnEmployee(int controleID)
        {
            return Task.CompletedTask;
        }
    }
}

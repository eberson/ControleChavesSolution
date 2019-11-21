using AutoMapper;
using ControleChaves.Application.Entities;
using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<Funcionario, FuncionarioViewModel>();
            CreateMap<Localizacao, LocalizacaoViewModel>();
            CreateMap<Chave, ChaveViewModel>();
            CreateMap<UsuarioViewModel, EditUsuarioViewModel>();
        }
    }
}

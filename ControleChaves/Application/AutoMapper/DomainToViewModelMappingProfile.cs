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

            CreateMap<Localizacao, LocalizacaoViewModel>()
                .ForMember(vm => vm.Status, opts => opts.MapFrom(src => src.Status.ToString()));

            CreateMap<Chave, ChaveViewModel>()
                .ForMember(vm => vm.LocalizacaoID, opts => opts.MapFrom(src => src.Localizacao.ID))
                .ForMember(vm => vm.Status, opts => opts.MapFrom(src => src.Status.ToString()));

            CreateMap<UsuarioViewModel, EditUsuarioViewModel>();

            CreateMap<Controle, ControleViewModel>();
        }
    }
}

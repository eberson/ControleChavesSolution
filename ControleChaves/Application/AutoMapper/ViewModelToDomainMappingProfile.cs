using AutoMapper;
using ControleChaves.Application.Entities;
using ControleChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleChaves.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioViewModel, Usuario>()
                .ForMember(u => u.Retiradas, opts => opts.Ignore())
                .ForMember(u => u.Devolucoes, opts => opts.Ignore())
                .ForMember(u => u.Status, opts => opts.NullSubstitute(Status.ATIVO));

            CreateMap<EditUsuarioViewModel, UsuarioViewModel>()
                .ForMember(u => u.Senha, opts => opts.Ignore());

            CreateMap<FuncionarioViewModel, Funcionario>()
                .ForMember(f => f.Retiradas, opts => opts.Ignore())
                .ForMember(f => f.Devolucoes, opts => opts.Ignore())
                .ForMember(u => u.Status, opts => opts.NullSubstitute(Status.ATIVO));

            CreateMap<LocalizacaoViewModel, Localizacao>()
                .ForMember(l => l.Status, opts => opts.Ignore());

            CreateMap<ChaveViewModel, Chave>()
                .ForMember(c => c.Localizacao, opts => opts.Ignore())
                .ForMember(c => c.Movimentacoes, opts => opts.Ignore())
                .ForMember(c => c.Status, opts => opts.Ignore());
        }
    }
}

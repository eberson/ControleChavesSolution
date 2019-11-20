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
                .ForMember(u => u.Devolucoes, opts => opts.Ignore());

            CreateMap<EditUsuarioViewModel, UsuarioViewModel>()
                .ForMember(u => u.Senha, opts => opts.Ignore());
        }
    }
}

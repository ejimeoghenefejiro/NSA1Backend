using AutoMapper;
using NSA1.Core.Dto.CreateViewModels;
using NSA1.Core.Dto.EntityModels;
using NSA1.Core.Dto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Utilities
{
     public class MappingProfile : Profile
     {
        public MappingProfile()
        {
            CreateMap<Club, CreateClubView>().ReverseMap();
            CreateMap<Club, ClubDetailView>().ReverseMap();
            CreateMap<ModelProfile, CreateModelView>().ReverseMap();
            CreateMap<ModelsDetailsView, ModelsDetailsView>().ReverseMap();
            CreateMap<MemberProfile, CreateMemberView>().ReverseMap();
            CreateMap<MemberProfile, MemberDetailView>().ReverseMap();
        }
     }
}

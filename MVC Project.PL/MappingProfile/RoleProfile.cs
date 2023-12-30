using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVC_Project.PL.ViewModels;

namespace MVC_Project.PL.MappingProfile
{
    public class RoleProfile:Profile
    {

        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.RoleName))
                .ReverseMap();
        }
    }
}

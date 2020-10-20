using AutoMapper;
using IdentityService.Core.DTOs;
using IdentityService.Core.DTOs.Account;
using IdentityService.Core.Entities;

namespace IdentityService.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(PagedResultDto<>), typeof(PagedResultDto<>));
            CreateMap<PagedResultDto<ApplicationUser>, PagedResultDto<LoginDto>>();
            CreateMap<ApplicationUser, LoginDto>();
            CreateMap<LoginDto, ApplicationUser>();
        }
    }
}
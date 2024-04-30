using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserApi.Dtos.Responses;
using UserDomain.Entities;

namespace UserApi.AutoMapper
{
	public class DomainToResponseProfile : Profile
	{
        public DomainToResponseProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<IdentityUser, UserResponse>();
		}
    }
}

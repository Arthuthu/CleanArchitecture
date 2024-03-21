using AutoMapper;
using UserApi.Dtos.Responses;
using UserDomain.Entities;

namespace UserApi.AutoMapper
{
	public class DomainToResponseProfile : Profile
	{
        public DomainToResponseProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}

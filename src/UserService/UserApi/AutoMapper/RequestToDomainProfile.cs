using AutoMapper;
using UserApi.Dtos.Requests;
using UserDomain.Entities;

namespace UserApp.AutoMapper
{
	public sealed class RequestToDomainProfile : Profile
	{
        public RequestToDomainProfile()
        {
            CreateMap<UserRequest, User>(MemberList.Destination);
        }
    }
}

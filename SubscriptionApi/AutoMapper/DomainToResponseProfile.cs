using AutoMapper;
using SubscriptionApi.Dtos.Responses;
using SubscriptionDomain.Entities;

namespace SubscriptionApi.AutoMapper
{
	public class DomainToResponseProfile : Profile
	{
        public DomainToResponseProfile()
        {
            CreateMap<Subscription, SubscriptionResponse>(MemberList.Destination);
        }
    }
}

using AutoMapper;
using SubscriptionApi.Dtos.Requests;
using SubscriptionDomain.Entities;

namespace SubscriptionApi.AutoMapper
{
	public class RequestToDomainProfile : Profile
	{
        public RequestToDomainProfile()
        {
            CreateMap<SubscriptionRequest, Subscription>(MemberList.Destination);
            CreateMap<ContractRequest, Contract>(MemberList.Destination);
        }
    }
}

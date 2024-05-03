using AutoMapper;
using TreinoApi.Dtos.Requests;
using TreinoDomain.Entities;

namespace SubscriptionApi.AutoMapper
{
	public class RequestToDomainProfile : Profile
	{
        public RequestToDomainProfile()
        {
            CreateMap<ExercicioRequest, Exercicio>(MemberList.Destination);
            CreateMap<TreinoRequest, Treino>(MemberList.Destination);
        }
    }
}

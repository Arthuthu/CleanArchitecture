using AutoMapper;
using TreinoApi.Dtos.Responses;
using TreinoDomain.Entities;

namespace SubscriptionApi.AutoMapper
{
	public class DomainToResponseProfile : Profile
	{
        public DomainToResponseProfile()
        {
            CreateMap<Exercicio, ExercicioResponse>(MemberList.Destination);
            CreateMap<Treino, TreinoResponse>(MemberList.Destination);
        }
    }
}

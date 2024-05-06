using TreinoDomain.Entities;

namespace TreinoApplication.Abstractions
{
    public interface IExercicioService
    {
        Task<Exercicio> Add(Exercicio exercicio, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<List<Exercicio>> Get(CancellationToken cancellationToken);
        Task<Exercicio?> Get(Guid id, CancellationToken cancellationToken);
        Task<Exercicio?> Update(Exercicio exercicio, CancellationToken cancellationToken);
    }
}
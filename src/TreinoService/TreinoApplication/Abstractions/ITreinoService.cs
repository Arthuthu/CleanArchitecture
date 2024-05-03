using TreinoDomain.Entities;

namespace TreinoApplication.Abstractions
{
    public interface ITreinoService
    {
        Task<Treino> Add(Treino treino, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<List<Treino>> Get(CancellationToken cancellationToken);
        Task<Treino?> Get(Guid id, CancellationToken cancellationToken);
        Task<Treino?> Update(Treino treino, CancellationToken cancellationToken);
    }
}
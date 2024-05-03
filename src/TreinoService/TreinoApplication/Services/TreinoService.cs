using TreinoApplication.Abstractions;
using TreinoDomain.Entities;

namespace TreinoApplication.Services
{
    public sealed class TreinoService : ITreinoService
	{
		private readonly ITreinoRepository _repository;

		public TreinoService(ITreinoRepository repository)
		{
			_repository = repository;
		}

		public async Task<Treino> Add(Treino treino, CancellationToken cancellationToken)
		{
			return await _repository.Add(treino, cancellationToken);
		}

		public async Task<Treino?> Get(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Get(id, cancellationToken);
		}

		public async Task<List<Treino>> Get(CancellationToken cancellationToken)
		{
			return await _repository.Get(cancellationToken);
		}

		public async Task<Treino?> Update(Treino treino, CancellationToken cancellationToken)
		{
			return await _repository.Update(treino, cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}
	}
}

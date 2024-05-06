using TreinoApplication.Abstractions;
using TreinoDomain.Entities;
using TreinoInfra.Repositories;

namespace TreinoApplication.Services
{
    public sealed class ExercicioService : IExercicioService
	{
		private readonly IExercicioRepository _repository;

		public ExercicioService(IExercicioRepository repository)
		{
			_repository = repository;
		}

		public async Task<Exercicio> Add(Exercicio exercicio, CancellationToken cancellationToken)
		{
			return await _repository.Add(exercicio, cancellationToken);
		}

		public async Task<Exercicio?> Get(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Get(id, cancellationToken);
		}

		public async Task<List<Exercicio>> Get(CancellationToken cancellationToken)
		{
			return await _repository.Get(cancellationToken);
		}

		public async Task<Exercicio?> Update(Exercicio exercicio, CancellationToken cancellationToken)
		{
			return await _repository.Update(exercicio, cancellationToken);
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.Delete(id, cancellationToken);
		}
	}
}

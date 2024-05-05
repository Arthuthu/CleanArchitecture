using Microsoft.EntityFrameworkCore;
using TreinoDomain.Entities;
using TreinoInfra.Context;

namespace TreinoInfra.Repositories
{
	public sealed class ExercicioRepository : IExercicioRepository
	{
		private readonly ApplicationDbContext _context;

		public ExercicioRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Exercicio> Add(Exercicio exercicio, CancellationToken cancellationToken)
		{
			_context.Exercicios.Add(exercicio);
			await _context.SaveChangesAsync(cancellationToken);

			return exercicio;
		}

		public async Task<Exercicio?> Get(Guid id, CancellationToken cancellationToken)
		{
			Exercicio? exercicio = await _context.Exercicios.FindAsync(id, cancellationToken);
			return exercicio;
		}

		public async Task<List<Exercicio>> Get(CancellationToken cancellationToken)
		{
			List<Exercicio> exercicios = await _context.Exercicios.ToListAsync(cancellationToken);
			return exercicios;
		}

		public async Task<Exercicio?> Update(Exercicio exercicio, CancellationToken cancellationToken)
		{
			Exercicio? requestedExercicio = await _context.Exercicios
				.SingleOrDefaultAsync(x => x.Id == exercicio.Id);

			if (requestedExercicio is null)
			{
				return null;
			}

			requestedExercicio.Nome = requestedExercicio.Nome;

			_context.Exercicios.Update(requestedExercicio);
			await _context.SaveChangesAsync(cancellationToken);

			return requestedExercicio;
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int affectedRows = await _context.Exercicios.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}
	}
}

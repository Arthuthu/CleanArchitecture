using Microsoft.EntityFrameworkCore;
using TreinoApplication.Abstractions;
using TreinoDomain.Entities;
using TreinoInfra.Context;

namespace TreinoInfra.Repositories
{
	public sealed class TreinoRepository : ITreinoRepository
	{
		private readonly TreinoContext _context;

		public TreinoRepository(TreinoContext context)
		{
			_context = context;
		}

		public async Task<Treino> Add(Treino treino, CancellationToken cancellationToken)
		{
			_context.Treinos.Add(treino);
			await _context.SaveChangesAsync(cancellationToken);

			return treino;
		}

		public async Task<Treino?> Get(Guid id, CancellationToken cancellationToken)
		{
			Treino? treino = await _context.Treinos.FindAsync(id, cancellationToken);
			return treino;
		}

		public async Task<List<Treino>> Get(CancellationToken cancellationToken)
		{
			List<Treino> treinos = await _context.Treinos.ToListAsync(cancellationToken);
			return treinos;
		}

		public async Task<Treino?> Update(Treino treino, CancellationToken cancellationToken)
		{
			Treino? requestedTreino = await _context.Treinos
				.SingleOrDefaultAsync(x => x.Id == treino.Id);

			if (requestedTreino is null)
			{
				return null;
			}

			requestedTreino.Nome = treino.Nome;

			_context.Treinos.Update(requestedTreino);
			await _context.SaveChangesAsync(cancellationToken);

			return requestedTreino;
		}

		public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
		{
			int affectedRows = await _context.Treinos.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
			return affectedRows > 0;
		}
	}
}

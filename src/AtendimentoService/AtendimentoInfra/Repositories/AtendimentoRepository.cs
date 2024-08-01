﻿using AtendimentoDomain.Entities;
using AtendimentoInfra.Context;
using Microsoft.EntityFrameworkCore;

namespace AtendimentoInfra.Repositories
{
    public sealed class AtendimentoRepository
    {
        private readonly AtendimentoContext _context;

        public AtendimentoRepository(AtendimentoContext context)
        {
            _context = context;
        }

        public async Task<Atendimento?> GetAtendimentosById(Guid atendimentoId, CancellationToken ct)
        {
            Atendimento? atendimento = await _context.Atendimento.FindAsync(atendimentoId, ct);
            return atendimento;
        }

        public async Task<List<Atendimento>?> GetAtendimentosByUserId(Guid userId, CancellationToken ct)
        {
            List<Atendimento> atendimentos = await _context.Atendimento.Where(x => x.UserId == userId).ToListAsync(ct);
            return atendimentos;
        }

        public async Task<Atendimento> Add(Atendimento atendimento, CancellationToken ct)
        {
            await _context.AddAsync(atendimento, ct);
            await _context.SaveChangesAsync(ct);

            return atendimento;
        }

        public async Task<bool> Delete(Guid id, CancellationToken ct)
        {
            int affectedRows = await _context.Atendimento.Where(x => x.Id == id).ExecuteDeleteAsync(ct);
            return affectedRows > 0;
        }
    }
}

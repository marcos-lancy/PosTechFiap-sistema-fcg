using Fcg.Domain.Entities;
using Fcg.Domain.Interfaces;
using Fcg.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fcg.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> AdicionarAsync(T entidade)
    {
        await _dbSet.AddAsync(entidade);
        await _context.SaveChangesAsync();

        return entidade;
    }

    public async Task Atualizar(T entidade)
    {
        _dbSet.Update(entidade);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(T entidade)
    {
        _dbSet.Remove(entidade);
        await _context.SaveChangesAsync();
    }
}
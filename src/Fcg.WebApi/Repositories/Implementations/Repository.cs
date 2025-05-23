using Fcg.WebApi.Data.Context;
using Fcg.WebApi.Models;
using Fcg.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fcg.WebApi.Repositories.Implementations;

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

    public async Task AdicionarAsync(T entidade)
    {
        await _dbSet.AddAsync(entidade);
    }

    public void Atualizar(T entidade)
    {
        _dbSet.Update(entidade);
    }

    public void Remover(T entidade)
    {
        _dbSet.Remove(entidade);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
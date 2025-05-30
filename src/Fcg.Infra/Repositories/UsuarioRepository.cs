using Fcg.Domain.Entities;
using Fcg.Domain.Interfaces;
using Fcg.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fcg.Infra.Repositories;
public class UsuarioRepository(AppDbContext context) : Repository<UsuarioEntity>(context), IUsuarioRepository
{
    public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}

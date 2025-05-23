using Fcg.WebApi.Models;
using Fcg.WebApi.Repositories.Interfaces;
using Fcg.WebApi.Services.Interfaces;

namespace Fcg.WebApi.Services;

public class JogoService : IJogoService
{
    private readonly IRepository<JogoEntity> _jogoRepository;

    public JogoService(IRepository<JogoEntity> jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }

    public async Task<IEnumerable<JogoEntity>> ObterTodosAsync()
    {
        return await _jogoRepository.ObterTodosAsync();
    }

    public async Task<JogoEntity?> ObterPorIdAsync(Guid id)
    {
        return await _jogoRepository.ObterPorIdAsync(id);
    }

    public async Task<JogoEntity> CriarUsuarioAsync(JogoEntity jogo)
    {
        await _jogoRepository.AdicionarAsync(jogo);
        await _jogoRepository.SaveChangesAsync();
        return jogo;
    }

    public async Task AtualizarUsuarioAsync(JogoEntity jogo)
    {
        _jogoRepository.Atualizar(jogo);
        await _jogoRepository.SaveChangesAsync();
    }

    public async Task RemoverUsuarioAsync(Guid id)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(id);
        if (jogo != null)
        {
            _jogoRepository.Remover(jogo);
            await _jogoRepository.SaveChangesAsync();
        }
    }
}
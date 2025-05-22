using Fcg.WebApi.Models;
using Fcg.WebApi.Repositorues.Interfaces;
using Fcg.WebApi.Services.Interfaces;

namespace Fcg.WebApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IRepository<UsuarioEntity> _usuarioRepository;

    public UsuarioService(IRepository<UsuarioEntity> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    public async Task<IEnumerable<UsuarioEntity>> ObterTodosAsync()
    {
        return await _usuarioRepository.ObterTodosAsync();
    }

    public async Task<UsuarioEntity?> ObterPorIdAsync(Guid id)
    {
        return await _usuarioRepository.ObterPorIdAsync(id);
    }

    public async Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario)
    {
        await _usuarioRepository.AdicionarAsync(usuario);
        await _usuarioRepository.SaveChangesAsync();
        return usuario;
    }

    public async Task AtualizarUsuarioAsync(UsuarioEntity usuario)
    {
        _usuarioRepository.Atualizar(usuario);
        await _usuarioRepository.SaveChangesAsync();
    }

    public async Task RemoverUsuarioAsync(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario != null)
        {
            _usuarioRepository.Remover(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
    }
}
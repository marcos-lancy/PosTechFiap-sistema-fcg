using Fcg.Application.Dtos;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Interfaces;

namespace Fcg.Application.AppServices;

public class JogoAppService : IJogoAppService
{
    private readonly IRepository<JogoEntity> _jogoRepository;

    public JogoAppService(IRepository<JogoEntity> jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }

    public async Task<IEnumerable<JogoDto>> ObterTodosAsync()
    {
        var dados = await _jogoRepository.ObterTodosAsync();
        return dados.Select(x => new JogoDto
        {
            Id = x.Id,
            Nome = x.Nome,
            Descricao = x.Descricao
        });
    }

    public async Task<JogoDto?> ObterPorIdAsync(Guid id)
    {
        var dado = await _jogoRepository.ObterPorIdAsync(id);

        if (dado != null)
        {
            return new JogoDto()
            {
                Id = dado.Id,
                Nome = dado.Nome,
                Descricao = dado.Descricao,
            };
        }

        return null;
    }

    public async Task<JogoDto> CadastrarAsync(CadastrarJogoRequest jogo)
    {
        var retorno = await _jogoRepository.AdicionarAsync(
            new JogoEntity(
                jogo.Nome,
                jogo.Descricao,
                jogo.Preco));
        
        return new JogoDto() 
        { 
            Id = retorno.Id,
            Nome = retorno.Nome,
            Descricao = retorno.Descricao,
            Preco = retorno.Preco,
        };
    }

    public async Task AtualizarAsync(JogoDto jogo)
    {
        await _jogoRepository.Atualizar(
            new JogoEntity());
    }

    public async Task RemoverAsync(Guid id)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(id);
        
        if (jogo != null)
            await _jogoRepository.Remover(jogo);
    }
}
using Fcg.Application.Dtos.Jogo;
using Fcg.Application.Dtos.Promocao;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Exceptions;
using Fcg.Domain.Interfaces;

namespace Fcg.Application.AppServices;

public class PromocaoAppService : IPromocaoAppService
{
    private readonly IRepository<PromocaoEntity> _repository;
    private readonly IRepository<JogoEntity> _jogoRepository;

    public PromocaoAppService(IRepository<PromocaoEntity> repository, IRepository<JogoEntity> jogoRepository)
    {
        _repository = repository;
        _jogoRepository = jogoRepository;
    }

    public async Task<IEnumerable<PromocaoDto>> ObterTodosAsync()
    {
        var dados = await _repository.ObterAsync();
        return dados.Select(u => new PromocaoDto
        {
            Id = u.Id,
            Jogo = new JogoDto
            {
                Id = u.Jogo.Id,
                Nome = u.Jogo.Nome,
                Descricao = u.Jogo.Descricao,
                Preco = u.Jogo.Preco,
            },
            Ativo = u.Ativa,
            Inicio = u.DataInicio,
            Final = u.DataFim,
            PrecoPromocional = u.PrecoPromocional
        });
    }

    public async Task<PromocaoDto> ObterPorIdAsync(Guid id)
    {
        var dado = await _repository.ObterPorIdAsync(id);
        if (dado == null)
            throw new NotFoundException();

        return new PromocaoDto
        {
            Id = dado.Id,
            Jogo = new JogoDto
            {
                Id = dado.Jogo.Id,
                Nome = dado.Jogo.Nome,
                Descricao = dado.Jogo.Descricao,
                Preco = dado.Jogo.Preco,
            },
            Ativo = dado.Ativa,
            Inicio = dado.DataInicio,
            Final = dado.DataFim,
            PrecoPromocional = dado.PrecoPromocional
        };
    }

    public async Task<PromocaoDto> CadastrarAsync(CadastrarPromocaoDto dto)
    {
        var dbData = await _jogoRepository.ObterPorIdAsync(dto.IdJogo);
        if (dbData is null || !dbData.Ativo)
            throw new NotFoundException("Não foi possível obter dados do jogo selecionado.");

        var promocao = new PromocaoEntity
        {
            JogoId = dto.IdJogo,
            PrecoPromocional = dto.PrecoPromocional,
            DataInicio = DateTime.SpecifyKind(dto.Inicio, DateTimeKind.Utc),
            DataFim = DateTime.SpecifyKind(dto.Final, DateTimeKind.Utc),
        };

        var dado = await _repository.AdicionarAsync(promocao);

        return new PromocaoDto
        {
            Id = dado.Id,
            Jogo = new JogoDto
            {
                Id = dado.Jogo.Id,
                Nome = dado.Jogo.Nome,
                Descricao = dado.Jogo.Descricao,
                Preco = dado.Jogo.Preco,
            },
            Ativo = dado.Ativa,
            Inicio = dado.DataInicio,
            Final = dado.DataFim,
            PrecoPromocional = dado.PrecoPromocional
        };
    }

    public async Task RemoverAsync(Guid id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        if (usuario == null)
            throw new NotFoundException();
        
        await _repository.Remover(usuario);
    }
}
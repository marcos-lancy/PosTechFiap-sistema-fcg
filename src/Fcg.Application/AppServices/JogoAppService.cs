using Fcg.Application.Dtos.Jogo;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Exceptions;
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
        var dados = await _jogoRepository.ObterAsync();
        return dados.Select(x => new JogoDto
        {
            Id = x.Id,
            Nome = x.Nome,
            Descricao = x.Descricao,
            Preco = x.Preco,
            Ativo = x.Ativo,
        });
    }

    public async Task<JogoDto?> ObterPorIdAsync(Guid id)
    {
        var dado = await _jogoRepository.ObterPorIdAsync(id);

        if (dado == null)
            throw new NotFoundException();

        return new JogoDto()
        {
            Id = dado.Id,
            Nome = dado.Nome,
            Descricao = dado.Descricao,
            Preco = dado.Preco,
            Ativo = dado.Ativo,
        };
    }

    public async Task<JogoDto> CadastrarAsync(CadastrarJogoDto dto)
    {
        var dbData = await _jogoRepository.ObterAsync(x => x.Nome == dto.Nome);
        if (dbData?.Count > 0)
            throw new ConflictException("Um jogo com este nome já está cadastrado no sistema.");

        var retorno = await _jogoRepository.AdicionarAsync(
            new JogoEntity(
                dto.Nome,
                dto.Descricao,
                dto.Preco));

        return new JogoDto()
        {
            Id = retorno.Id,
            Nome = retorno.Nome,
            Descricao = retorno.Descricao,
            Preco = retorno.Preco,
            Ativo = retorno.Ativo,
        };
    }

    public async Task AtualizarAsync(Guid id, AtualizarJogoDto dto)
    {
        var dbData = await _jogoRepository.ObterPorIdAsync(id);
        if (dbData is null)
            throw new NotFoundException();

        var dbDataEmail = await _jogoRepository.ObterAsync(x => x.Id != id && x.Nome == dto.Nome);
        if (dbDataEmail?.Count != 0)
            throw new ConflictException("Um jogo com este nome já está cadastrado no sistema.");

        dbData.Nome = dto.Nome;
        dbData.Descricao = dto.Descricao;
        dbData.Preco = dto.Preco;

        await _jogoRepository.Atualizar(dbData);
    }

    public async Task RemoverAsync(Guid id)
    {
        var dbData = await _jogoRepository.ObterPorIdAsync(id);
        if (dbData is null)
            throw new NotFoundException();

        await _jogoRepository.Remover(dbData);
    }
}
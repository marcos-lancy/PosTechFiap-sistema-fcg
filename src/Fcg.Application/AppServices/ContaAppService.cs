using Fcg.Application.Dtos.Conta;
using Fcg.Application.Interfaces;
using Fcg.Domain.Exceptions;
using Fcg.Domain.Interfaces;

namespace Fcg.Application.AppServices;
public class ContaAppService : IContaAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioAutenticadoAppService _usuarioAutenticadoAppService;
    public ContaAppService(
        IUsuarioRepository usuarioRepository,
        IUsuarioAutenticadoAppService usuarioAutenticadoAppService)
    {
        _usuarioRepository = usuarioRepository;
        _usuarioAutenticadoAppService = usuarioAutenticadoAppService;
    }

    public async Task AtualizarAsync(AtualizarContaDto dto)
    {
        var usuario = await _usuarioAutenticadoAppService.ObterUsuarioAutenticadoAsync();

        var dbDataEmail = await _usuarioRepository.ObterAsync(x => x.Id != usuario.Id && x.Email == dto.Email);
        if (dbDataEmail?.Count != 0)
            throw new ConflictException("O endereço de e-mail informado já está cadastrado.");

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;

        await _usuarioRepository.Atualizar(usuario);
    }

    public async Task DescadastrarAsync()
    {
        await _usuarioRepository.Remover(
            await _usuarioAutenticadoAppService.ObterUsuarioAutenticadoAsync());
    }

    public async Task<ContaDto> ObterAsync()
    {
        var usuario = await _usuarioAutenticadoAppService.ObterUsuarioAutenticadoAsync();

        return new ContaDto()
        {
            Email = usuario.Email,
            Nome = usuario.Nome,
            Id = usuario.Id,
            Jogos = usuario.JogosAdquiridos.Select(jogo => new JogosContaDto
            {
                IdComprovante = jogo.Id,
                Nome = jogo.Jogo.Nome,
                Descricao = jogo.Jogo.Descricao,
                ValorPago = jogo.PrecoPago,
                DataAquisicao = jogo.DataAquisicao,
            }).ToList()
        };
    }
}

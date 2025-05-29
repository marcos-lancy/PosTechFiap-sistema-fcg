using Fcg.Application.Dtos.Compra;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Exceptions;
using Fcg.Domain.Interfaces;
using Fcg.Domain.Interfaces.Services;

namespace Fcg.Application.AppServices;
public class CompraAppService : ICompraAppService
{
    private readonly IRepository<JogoAdquiridoEntity> _repository;
    private readonly ICompraService _service;    
    private readonly IRepository<JogoEntity> _jogoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioAutenticadoAppService _usuarioAutenticadoAppService;

    public CompraAppService(
        IRepository<JogoAdquiridoEntity> repository,
        IUsuarioRepository usuarioRepository,
        IRepository<JogoEntity> jogoRepository,
        IUsuarioAutenticadoAppService usuarioAutenticadoAppService,
        ICompraService service)
    {
        _repository = repository;
        _usuarioRepository = usuarioRepository;
        _jogoRepository = jogoRepository;
        _usuarioAutenticadoAppService = usuarioAutenticadoAppService;
        _service = service;
    }

    public async Task<JogoAdquiridoDto> ComprarJogoAsync(ComprarJogoDto request)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(request.IdJogo);
        if (jogo == null)
            throw new NotFoundException("Jogo selecionado não foi encontrado.");

        var email =  _usuarioAutenticadoAppService.ObterEmail();
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
        if (usuario == null)
             throw new NotFoundException("Usuário autenticado não foi encontrado.");

        var valorJogo = _service.ObterValorPromocionalJogo(jogo);

        var jogoAdquirido = new JogoAdquiridoEntity()
        {
            JogoId = jogo.Id,
            UsuarioId = usuario.Id,
            DataAquisicao = DateTime.UtcNow,
            PrecoPago = valorJogo
        };

        var cadastro = await _repository.AdicionarAsync(jogoAdquirido);

        return new JogoAdquiridoDto()
        {
            IdComprovante = cadastro.Id,
            DataAquisicao = cadastro.DataAquisicao,
            PrecoPago = valorJogo
        };
    }
}

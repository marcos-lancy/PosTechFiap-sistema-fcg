using Fcg.Application.Dtos.Usuario;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Enums;
using Fcg.Domain.Exceptions;
using Fcg.Domain.Interfaces;

namespace Fcg.Application.AppServices;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtAppService _jwtAppService;

    public UsuarioAppService(
        IUsuarioRepository usuarioRepository,
        IJwtAppService jwtAppService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtAppService = jwtAppService;
    }

    public async Task<string> EfetuarLoginAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            throw new AuthenticationException("Houve um erro ao efetuar login, verifique os dados e tente novamente.");

        return _jwtAppService.GerarToken(usuario.Email, usuario.Role.ToString());
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodosAsync()
    {
        var dados = await _usuarioRepository.ObterAsync();
        return dados.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Role = u.Role,
            Email = u.Email
        });
    }

    public async Task<UsuarioDto?> ObterPorIdAsync(Guid id)
    {
        var dado = await _usuarioRepository.ObterPorIdAsync(id);
        if (dado == null)
            throw new NotFoundException();

        return new UsuarioDto
        {
            Id = dado.Id,
            Nome = dado.Nome,
            Email = dado.Email,
            Role = dado.Role,
        };
    }

    public async Task<UsuarioDto?> ObterPorEmailAsync(string email)
    {
        var dado = await _usuarioRepository.ObterPorEmailAsync(email);
        if (dado == null)
            throw new NotFoundException();

        return new UsuarioDto()
        {
            Id = dado.Id,
            Nome = dado.Nome,
            Email = dado.Email,
            Role = dado.Role,
        };
    }

    public async Task<UsuarioDto> CadastrarAsync(CadastrarUsuarioDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (usuario != null)
            throw new ConflictException("O endereço de e-mail informado já está cadastrado.");

        var novoUsuario = new UsuarioEntity
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Role = RoleEnum.Usuario
        };

        var registro = await _usuarioRepository.AdicionarAsync(novoUsuario);

        return new UsuarioDto
        {
            Id = registro.Id,
            Nome = registro.Nome,
            Role = registro.Role,
            Email = registro.Email
        };
    }

    public async Task AtualizarAsync(Guid id, AtualizarUsuarioDto dto)
    {
        var dbData = await _usuarioRepository.ObterPorIdAsync(id);
        if (dbData is null)
            throw new NotFoundException();

        var dbDataEmail = await _usuarioRepository.ObterAsync(x => x.Id != id && x.Email == dto.Email);
        if (dbDataEmail?.Count != 0)
            throw new ConflictException("O endereço de e-mail informado já está cadastrado.");

        dbData.Nome = dto.Nome;
        dbData.Email = dto.Email;

        await _usuarioRepository.Atualizar(dbData);
    }

    public async Task AtualizarRoleAsync(Guid id, RoleEnum role)
    {
        var dbData = await _usuarioRepository.ObterPorIdAsync(id);
        if (dbData is null)
            throw new NotFoundException();

        dbData.Role = role;

        await _usuarioRepository.Atualizar(dbData);
    }

    public async Task RemoverAsync(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null)
            throw new NotFoundException();
        
        await _usuarioRepository.Remover(usuario);
    }
}
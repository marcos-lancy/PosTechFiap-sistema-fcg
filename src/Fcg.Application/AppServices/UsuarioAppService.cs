using Fcg.Application.Dtos;
using Fcg.Application.Interfaces;
using Fcg.Domain.Entities;
using Fcg.Domain.Enums;
using Fcg.Domain.Interfaces;

namespace Fcg.Application.AppServices;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioAppService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    
    public async Task<IEnumerable<UsuarioDto>> ObterTodosAsync()
    {
        var dados = await _usuarioRepository.ObterTodosAsync();

        return dados.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Role = u.Role
        });
    }

    public async Task<UsuarioDto?> ObterPorIdAsync(Guid id)
    {
        var dado = await _usuarioRepository.ObterPorIdAsync(id);
        if (dado != null)
        {
            return new UsuarioDto
            {
                Id = dado.Id,
                Nome = dado.Nome,
                Role = dado.Role
            };
        }

        return null;
    }

    public async Task<UsuarioDto?> ObterPorEmailAsync(string email)
    {
        var dado = await _usuarioRepository.ObterPorEmailAsync(email);
        return new UsuarioDto()
        {
            Nome = dado.Nome,
            Email = dado.Email,
            SenhaHash = dado.SenhaHash,
            Role = dado.Role
        };
    }

    public async Task<UsuarioDto> CadastrarAsync(CadastrarUsuarioDto dto)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (usuario != null)
            throw new Exception("E-mail já cadastrado.");

        var novoUsuario = new UsuarioEntity
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Role = TipoPessoaEnum.Usuario
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

    public async Task AtualizarAsync(UsuarioDto usuario)
    {
        await _usuarioRepository.Atualizar(
            new UsuarioEntity());
    }

    public async Task RemoverAsync(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario != null)
        {
            await _usuarioRepository.Remover(usuario);
        }
    }
}
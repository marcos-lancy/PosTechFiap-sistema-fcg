using Fcg.Domain.Entities;

namespace Fcg.Domain.Interfaces.Services;

public interface ICompraService
{
    decimal ObterValorPromocionalJogo(JogoEntity jogo);
}
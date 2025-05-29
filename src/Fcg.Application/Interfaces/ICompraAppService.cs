using Fcg.Application.Dtos.Compra;

namespace Fcg.Application.Interfaces;

public interface ICompraAppService
{
    Task<JogoAdquiridoDto> ComprarJogoAsync(ComprarJogoDto request);
}
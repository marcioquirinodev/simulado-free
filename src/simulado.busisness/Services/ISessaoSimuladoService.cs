using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface ISessaoSimuladoService
{
    Task<IEnumerable<SessaoSimulado>> GetAllAsync();
    Task<SessaoSimulado?> GetByIdAsync(Guid id);
    Task<SessaoSimulado> CreateAsync(SessaoSimuladoViewModel vm);
    Task<bool> UpdateAsync(Guid id, SessaoSimuladoViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
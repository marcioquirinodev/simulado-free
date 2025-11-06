using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface ISimuladoService
{
    Task<IEnumerable<Simulado>> GetAllAsync();
    Task<Simulado?> GetByIdAsync(Guid id);
    Task<Simulado> CreateAsync(SimuladoViewModel vm);
    Task<bool> UpdateAsync(Guid id, SimuladoViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
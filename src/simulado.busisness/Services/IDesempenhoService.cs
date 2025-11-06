using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface IDesempenhoService
{
    Task<IEnumerable<Desempenho>> GetAllAsync();
    Task<Desempenho?> GetByIdAsync(Guid id);
    Task<Desempenho> CreateAsync(DesempenhoViewModel vm);
    Task<bool> UpdateAsync(Guid id, DesempenhoViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
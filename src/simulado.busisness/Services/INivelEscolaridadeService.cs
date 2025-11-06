using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface INivelEscolaridadeService
{
    Task<IEnumerable<NivelEscolaridade>> GetAllAsync();
    Task<NivelEscolaridade?> GetByIdAsync(Guid id);
    Task<NivelEscolaridade> CreateAsync(NivelEscolaridadeViewModel vm);
    Task<bool> UpdateAsync(Guid id, NivelEscolaridadeViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
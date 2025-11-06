using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface IDisciplinaService
{
    Task<IEnumerable<Disciplina>> GetAllAsync();
    Task<Disciplina?> GetByIdAsync(Guid id);
    Task<Disciplina> CreateAsync(DisciplinaViewModel vm);
    Task<bool> UpdateAsync(Guid id, DisciplinaViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
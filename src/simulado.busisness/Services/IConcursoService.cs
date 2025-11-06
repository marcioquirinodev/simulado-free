using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface IConcursoService
{
    Task<IEnumerable<Concurso>> GetAllAsync();
    Task<Concurso?> GetByIdAsync(Guid id);
    Task<Concurso> CreateAsync(ConcursoViewModel vm);
    Task<bool> UpdateAsync(Guid id, ConcursoViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
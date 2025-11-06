using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(Guid id);
    Task<Categoria> CreateAsync(CategoriaViewModel vm);
    Task<bool> UpdateAsync(Guid id, CategoriaViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
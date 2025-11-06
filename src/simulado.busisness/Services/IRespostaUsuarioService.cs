using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.busisness.Services;

public interface IRespostaUsuarioService
{
    Task<IEnumerable<RespostaUsuario>> GetAllAsync();
    Task<RespostaUsuario?> GetByIdAsync(Guid id);
    Task<RespostaUsuario> CreateAsync(RespostaUsuarioViewModel vm);
    Task<bool> UpdateAsync(Guid id, RespostaUsuarioViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
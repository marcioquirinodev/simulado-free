using Microsoft.AspNetCore.Identity;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.business.Services;

public interface IUsuarioService
{
    Task<IdentityResult> RegisterAsync(RegisterUsuarioViewModel vm);
    Task<IdentityResult> UpdateAsync(Guid id, UpdateUsuarioViewModel vm);
    Task<IdentityResult> ChangePasswordAsync(Guid id, ChangePasswordViewModel vm);
    Task<IdentityResult> DeleteAsync(Guid id);
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<IEnumerable<Usuario>> GetAllAsync();
}
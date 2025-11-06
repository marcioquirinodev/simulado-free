using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using simulado.shared.Entidades;

namespace simulado.business.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using simulado.shared.Entidades;

namespace simulado.business.Services;

public interface IUsuarioService
{
    Task<IdentityResult> RegisterAsync(RegisterUsuarioDto dto);
    Task<IdentityResult> UpdateAsync(Guid id, UpdateUsuarioDto dto);
    Task<IdentityResult> ChangePasswordAsync(Guid id, ChangePasswordDto dto);
    Task<IdentityResult> DeleteAsync(Guid id);
    Task<Usuario?> GetByIdAsync(Guid id);
    Task<IEnumerable<Usuario>> GetAllAsync();
}
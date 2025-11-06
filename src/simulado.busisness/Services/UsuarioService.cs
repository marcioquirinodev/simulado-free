using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.business.Services;

public class UsuarioService : IUsuarioService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UsuarioService(UserManager<Usuario> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterUsuarioViewModel vm)
    {
        if (await _userManager.FindByEmailAsync(vm.Email) != null)
            return IdentityResult.Failed(new IdentityError { Description = "Email já cadastrado." });

        var user = new Usuario
        {
            UserName = string.IsNullOrWhiteSpace(vm.UserName) ? vm.Email : vm.UserName,
            Email = vm.Email,
            NivelEscolaridadeId = vm.NivelEscolaridadeId,
            DataCadastro = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, vm.Password);
        if (!result.Succeeded) return result;

        // Garantir roles padrão existem e atribuir "Aluno" automaticamente
        var defaultRole = "Aluno";
        if (!await _roleManager.RoleExistsAsync(defaultRole))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(defaultRole));
        }

        // Sempre adicionar Aluno ao novo usuário (se vm.Roles vier com roles, também adiciona as delas)
        var rolesToAdd = new List<string> { defaultRole };
        if (vm.Roles != null && vm.Roles.Any())
        {
            // cria qualquer role adicional necessária
            foreach (var role in vm.Roles.Distinct())
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
                rolesToAdd.Add(role);
            }
        }

        result = await _userManager.AddToRolesAsync(user, rolesToAdd.Distinct());
        return result;
    }

    public async Task<IdentityResult> UpdateAsync(Guid id, UpdateUsuarioViewModel vm)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        if (!string.Equals(user.Email, vm.Email, StringComparison.OrdinalIgnoreCase))
        {
            var setEmail = await _userManager.SetEmailAsync(user, vm.Email);
            if (!setEmail.Succeeded) return setEmail;
        }

        user.UserName = string.IsNullOrWhiteSpace(vm.UserName) ? user.UserName : vm.UserName;
        user.NivelEscolaridadeId = vm.NivelEscolaridadeId;

        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ChangePasswordAsync(Guid id, ChangePasswordViewModel vm)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        if (!string.IsNullOrWhiteSpace(vm.OldPassword))
        {
            return await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
        }
        else
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, vm.NewPassword);
        }
    }

    public async Task<IdentityResult> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        return await _userManager.DeleteAsync(user);
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return _userManager.Users.ToList();
    }
}
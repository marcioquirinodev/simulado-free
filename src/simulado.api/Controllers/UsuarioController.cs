using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using simulado.shared.Models;
using simulado.business.Services;
using simulado.shared.Entidades;
using Microsoft.AspNetCore.Identity;

namespace simulado.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly UserManager<Usuario> _userManager;

    public UsuarioController(IUsuarioService usuarioService, UserManager<Usuario> userManager)
    {
        _usuarioService = usuarioService;
        _userManager = userManager;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUsuarioViewModel vm)
    {
        var result = await _usuarioService.RegisterAsync(vm);
        if (!result.Succeeded) return BadRequest(result.Errors);
        var user = await _userManager.FindByEmailAsync(vm.Email);
        return CreatedAtAction(nameof(GetById), new { id = user?.Id }, null);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _usuarioService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(new { user.Id, user.UserName, user.Email, user.NivelEscolaridadeId, user.DataCadastro });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _usuarioService.GetAllAsync();
        return Ok(users);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioViewModel vm)
    {
        var result = await _usuarioService.UpdateAsync(id, vm);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return NoContent();
    }

    [HttpPost("{id:guid}/change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordViewModel vm)
    {
        var result = await _usuarioService.ChangePasswordAsync(id, vm);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _usuarioService.DeleteAsync(id);
        if (!result.Succeeded) return BadRequest(result.Errors);
        return NoContent();
    }
}
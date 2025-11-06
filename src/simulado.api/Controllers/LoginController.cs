using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using simulado.shared.Entidades;
using simulado.shared.Models;

namespace simulado.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public LoginController(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("token")]
    public async Task<IActionResult> Token([FromBody] LoginRequestViewModel vm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null) return Unauthorized();

        var signIn = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, lockoutOnFailure: false);
        if (!signIn.Succeeded) return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key") ?? throw new InvalidOperationException("JWT Key missing");
        var issuer = jwtSection.GetValue<string>("Issuer") ?? "simulado";
        var audience = jwtSection.GetValue<string>("Audience") ?? "simulado_clients";
        var expireMinutes = jwtSection.GetValue<int?>("ExpireMinutes") ?? 60;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }.ToList();

        // adicionar roles como claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var keyBytes = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: signingCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return Ok(new LoginResponseViewModel
        {
            Token = tokenString,
            ExpiresAt = tokenDescriptor.ValidTo
        });
    }
}
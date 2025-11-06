namespace simulado.shared.Models;

public class RegisterUsuarioViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public Guid NivelEscolaridadeId { get; set; }
    public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
}

public class UpdateUsuarioViewModel
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public Guid NivelEscolaridadeId { get; set; }
}

public class ChangePasswordViewModel
{
    // if provided do ChangePassword; if empty do admin ResetPassword
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
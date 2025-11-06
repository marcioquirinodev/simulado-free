using System;

namespace simulado.shared.Models;

public class LoginRequestViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseViewModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
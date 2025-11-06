using System;

namespace simulado.shared.Models;

public class SessaoSimuladoViewModel
{
    public Guid SimuladoId { get; set; }
    public Guid UsuarioId { get; set; }
    // DataInicio fornecida pelo cliente ou atribuída no service
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    // DataFim pode ser nula até o término da sessão
    public DateTime? DataFim { get; set; }
    public double Pontuacao { get; set; }
}
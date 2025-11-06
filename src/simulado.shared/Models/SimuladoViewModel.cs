using System;

namespace simulado.shared.Models;

public class SimuladoViewModel
{
    public Guid ConcursoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    // DataCriacao é gerada pelo service; opcional no envio do cliente
    public DateTime DataCriacao { get; set; }
}
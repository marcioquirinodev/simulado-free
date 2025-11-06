using System;

namespace simulado.shared.Models;

public class RespostaUsuarioViewModel
{
    public Guid SessaoSimuladoId { get; set; }
    public Guid QuestaoId { get; set; }
    public string RespostaDada { get; set; } = string.Empty;
    public bool EstaCorreta { get; set; }
    public DateTime DataResposta { get; set; } = DateTime.UtcNow;
}
using System;

namespace simulado.shared.Models;

public class QuestaoViewModel
{
    public string TextoPergunta { get; set; } = string.Empty;
    public string RespostaCerta { get; set; } = string.Empty;
    public string RespostaErradaUm { get; set; } = string.Empty;
    public string RespostaErradaDois { get; set; } = string.Empty;
    public string RespostaErradaTres { get; set; } = string.Empty;
    public string RespostaErradaQuatro { get; set; } = string.Empty;
    public Guid SimuladoId { get; set; }
    public Guid DisciplinaId { get; set; }
}
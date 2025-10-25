namespace simulado.shared.Entidades;

public class Questao :  Entidade
{
    public string TextoPergunta { get; set; }
    public string RespostaCerta { get; set; }
    public string RespostaErradaUm { get; set; }
    public string RespostaErradaDois { get; set; }
    public string RespostaErradaTres { get; set; }
    public string RespostaErradaQuatro { get; set; }
    public Guid SimuladoId { get; set; }
    public Guid DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }
    public Simulado Simulado { get; set; }
    public IEnumerable<RespostaUsuario> RespostasUsuario { get; set; }
}

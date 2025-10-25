namespace simulado.shared.Entidades;

public class RespostaUsuario :  Entidade
{
    public Guid SessaoSimuladoId { get; set; }
    public Guid QuestaoId { get; set; }
    public string RespostaDada { get; set; }
    public bool EstaCorreta { get; set; }
    public DateTime DataResposta { get; set; }
    public SessaoSimulado SessaoSimulado { get; set; }
    public Questao Questao { get; set; }

}

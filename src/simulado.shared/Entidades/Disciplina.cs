namespace simulado.shared.Entidades;

public class Disciplina :  Entidade
{
    public string Descricao { get; set; }
    public IEnumerable<Questao> Questoes { get; set; }
}

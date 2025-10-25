namespace simulado.shared.Entidades;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descrição { get; set; }
    public IEnumerable<Concurso> Concursos { get; set; }
}

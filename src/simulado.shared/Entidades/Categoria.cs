namespace simulado.shared.Entidades;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public IEnumerable<Concurso> Concursos { get; set; }
}

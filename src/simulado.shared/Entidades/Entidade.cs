namespace simulado.shared.Entidades;

public abstract class Entidade
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

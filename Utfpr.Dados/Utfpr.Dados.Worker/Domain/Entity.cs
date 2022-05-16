namespace Utfpr.Dados.Worker.Domain;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CadastradoEm { get; set; }

    public Entity()
    {
        
    }
    
    protected Entity(Guid id, DateTime cadastradoEm)
    {
        Id = id;
        CadastradoEm = cadastradoEm;
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity(DateTime cadastradoEm)
    {
        CadastradoEm = cadastradoEm;
    }
}
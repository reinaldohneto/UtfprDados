using System.Linq.Expressions;

namespace Utfpr.Dados.Worker.Domain;

public interface IRepository<T> where T : Entity
{
    Task<bool> Adicionar(T entity);
    Task<bool> Deletar(Guid id);
    Task<bool> Atualizar(T entity);
    Task<T?> ObterPorId(Guid id);
    Task<ICollection<T>> ObterTodos();
    Task<bool> Commit();
    Task<bool> Existe(Guid id);
    Task<bool> Existe(Expression<Func<T, bool>> predicado);
}
using System.Linq.Expressions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Data;

namespace Utfpr.Dados.API.Domain;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly DbSet<T> _dbSet;
    private readonly ApplicationContext _context;
    private readonly NotificationContext _notificationContext;

    public Repository(ApplicationContext context, NotificationContext notificationContext)
    {
        _context = context;
        _notificationContext = notificationContext;
        _dbSet = context.Set<T>();
    }

    public async Task<bool> Adicionar(T entity)
    {
        await _dbSet.AddAsync(entity);

        if (await Commit())
            return true;
        
        _notificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<bool> Deletar(Guid id)
    {
        var registro = await _dbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (registro == null)
        {
            _notificationContext.BadRequest(nameof(Mensagens.RegistroNaoEncontrado), 
                Mensagens.RegistroNaoEncontrado);
            return false;
        }
        
        _dbSet.Remove(registro);
        if (await Commit())
            return true;
        
        _notificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<bool> Atualizar(T entity)
    {
        _dbSet.Update(entity);
        if (await Commit())
            return true;
        
        _notificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<T?> ObterPorId(Guid id)
    {
        var registro = await _dbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (registro != null)
            return registro;
        
        return null;
    }

    public async Task<ICollection<T>> ObterTodos()
    {
        return await _dbSet
            .AsQueryable()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Commit()
    {
        return !(await _context.SaveChangesAsync() < 1);
    }

    public async Task<bool> Existe(Guid id)
    {
        return await _dbSet
            .Where(i => i.Id.Equals(id))
            .AnyAsync();
    }

    public async Task<bool> Existe(Expression<Func<T, bool>> predicado)
    {
        return await _dbSet
            .Where(predicado)
            .AnyAsync();
    }
}
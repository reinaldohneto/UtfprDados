using System.Linq.Expressions;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Data;

namespace Utfpr.Dados.API.Domain;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly DbSet<T> DbSet;
    protected readonly ApplicationContext Context;
    protected readonly NotificationContext NotificationContext;

    public Repository(ApplicationContext context, NotificationContext notificationContext)
    {
        Context = context;
        NotificationContext = notificationContext;
        DbSet = context.Set<T>();
    }

    public async Task<bool> Adicionar(T entity)
    {
        await DbSet.AddAsync(entity);

        if (await Commit())
            return true;
        
        NotificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<bool> Deletar(Guid id)
    {
        var registro = await DbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (registro == null)
        {
            NotificationContext.NotFound(nameof(Mensagens.RegistroNaoEncontradoGenerico), 
                Mensagens.RegistroNaoEncontradoGenerico);
            return false;
        }
        
        DbSet.Remove(registro);
        if (await Commit())
            return true;
        
        NotificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<bool> Atualizar(T entity)
    {
        DbSet.Update(entity);
        if (await Commit())
            return true;
        
        NotificationContext.BadRequest(nameof(Mensagens.ErroInterno), 
            Mensagens.ErroInterno);
        return false;
    }

    public async Task<T?> ObterPorId(Guid id)
    {
        var registro = await DbSet.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (registro != null)
            return registro;
        
        return null;
    }

    public async Task<ICollection<T>> ObterTodos()
    {
        return await DbSet
            .AsQueryable()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Commit()
    {
        return !(await Context.SaveChangesAsync() < 1);
    }

    public async Task<bool> Existe(Guid id)
    {
        return await DbSet
            .Where(i => i.Id.Equals(id))
            .AnyAsync();
    }

    public async Task<bool> Existe(Expression<Func<T, bool>> predicado)
    {
        return await DbSet
            .Where(predicado)
            .AnyAsync();
    }
}
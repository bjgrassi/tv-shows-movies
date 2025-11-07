using System.Linq.Expressions;

namespace Shared.Repository;

public interface IRepository<T>
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> Get(object id);
    Task<IEnumerable<T>> GetAll();
    Task<T> GetOneByCriteria(Expression<Func<T, bool>> expression);

    IQueryable<T> GetQueryable();
}
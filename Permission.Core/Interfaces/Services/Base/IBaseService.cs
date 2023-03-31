using Permission.Infra.Data.Models.Base;
using System.Linq.Expressions;

namespace Permission.Core.Interfaces.Services.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> Find(Expression<Func<T, bool>> predicate);
        Task<List<T>> Find(List<Expression<Func<T, bool>>> predicate);
        Task<List<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] include);
        Task<List<T>> Find(List<Expression<Func<T, bool>>> predicate, Expression<Func<T, object>>[] include);
        Task<T> Update(T entity);
        Task Delete(int id, int userId);
    }
}

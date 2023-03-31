using Permission.Infra.Data.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Permission.Infra.Data.Context;

namespace Permission.Core.Services.Base
{
    public class BaseService<T> : DataTableService<T>, IBaseService<T> where T : BaseEntity
    {
        private PermissionDBContext _context;
        private DbSet<T> _dbSet;

        public BaseService(PermissionDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            T entity = await this.GetById(id);
            entity.DeletedAt = DateTime.Now;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(x => x.DeletedAt == null).Where(predicate).ToListAsync();
        }

        public async Task<List<T>> Find(List<Expression<Func<T, bool>>> predicate)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(x => x.DeletedAt == null);

            foreach (var pdt in predicate)
                query = query.Where(pdt);

            return await query.ToListAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _dbSet;
            foreach (var property in include)
                query = query.Include(property);

            return await query.Where(x => x.DeletedAt == null).Where(predicate).ToListAsync();
        }

        public async Task<List<T>> Find(List<Expression<Func<T, bool>>> predicate, Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _dbSet;

            foreach (var property in include)
                query = query.Include(property);

            query = query.Where(x => x.DeletedAt == null);

            foreach (var pdt in predicate)
                query = query.Where(pdt);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.Where(x => x.DeletedAt == null).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.Where(x => x.DeletedAt == null && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

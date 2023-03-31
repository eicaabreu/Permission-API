using Permission.Core.Extensions.DataTable;
using Permission.Core.Models;
using Permission.Infra.Data.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Permission.Infra.Data.Context;
using Permission.Core.Interfaces.Services.Base;

namespace Permission.Core.Services.Base
{
    public class DataTableService<T> : IDataTableService<T> where T : BaseEntity
    {
        private PermissionDBContext _context;
        private DbSet<T> _dbSet;

        public DataTableService(PermissionDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<DtResult<T>> LoadDataTable(DtParameters dtParameters, string defaultOrder = "Id", Expression<Func<T, bool>> search = null, Expression<Func<T, object>>[] include = null, Expression<Func<T, bool>> queryBase = null, List<Expression<Func<T, bool>>> filters = null)
        {
            string orderCriteria = !string.IsNullOrEmpty(dtParameters.SortOrder)
                ? dtParameters.SortOrder
                : defaultOrder;

            bool orderAscendingDirection = dtParameters.Order != null
                ? dtParameters.Order[0].Dir.ToString().ToLower() == "asc"
                : true;

            IQueryable<T> query = _dbSet;

            if (include != null)
                foreach (var property in include)
                    query = query.Include(property);

            query = query.Where(x => x.DeletedAt == null);

            if (queryBase != null)
                query = _dbSet.Where(queryBase);

            int totalResultsCount = query.Count();

            if (search != null)
                query = query.Where(search);

            if (filters != null)
                foreach (var filter in filters)
                    query = query.Where(filter);

            query = orderAscendingDirection
                ? query.OrderByDynamic(orderCriteria, DtOrderDir.Asc)
                : query.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            int filteredResultsCount = query.Count();

            List<T> result = dtParameters.All ? await query.ToListAsync()
                : await query.Skip(dtParameters.Start).Take(dtParameters.Length).ToListAsync();

            return new DtResult<T>()
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = result
            };
        }
    }
}

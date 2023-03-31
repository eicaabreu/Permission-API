using Permission.Core.Models;
using Permission.Infra.Data.Models.Base;
using System.Linq.Expressions;

namespace Permission.Core.Interfaces.Services.Base
{
    public interface IDataTableService<T> where T : BaseEntity
    {
        Task<DtResult<T>> LoadDataTable(DtParameters dtParameters, string defaultOrder = "Id ", Expression<Func<T, bool>> search = null, Expression<Func<T, object>>[] include = null, Expression<Func<T, bool>> queryBase = null, List<Expression<Func<T, bool>>> filters = null);
    }
}

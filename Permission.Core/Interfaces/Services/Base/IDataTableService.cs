using Permission.Core.Models;
using Permission.Infra.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Core.Interfaces.Services.Base
{
    public interface IDataTableService<T> where T : BaseEntity
    {
        Task<DtResult<T>> LoadDataTable(DtParameters dtParameters, string defaultOrder = "Id ", Expression<Func<T, bool>> search = null, Expression<Func<T, object>>[] include = null, Expression<Func<T, bool>> queryBase = null, List<Expression<Func<T, bool>>> filters = null);
    }
}

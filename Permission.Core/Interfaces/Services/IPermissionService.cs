

using Permission.Core.DTOs;
using Permission.Core.Models;
using Permission.Core.Services.Base;

namespace Permission.Core.Interfaces.Services
{
    public interface IPermissionService : IBaseService<Permission.Infra.Data.Models.Permission>
    {
        public Task<Permission.Infra.Data.Models.Permission> CreatePermission(PermissionDto permissionDto);
        public Task<Permission.Infra.Data.Models.Permission> EditPermission(PermissionDto permissionDto);
        public Task<PaginatedList<Permission.Infra.Data.Models.Permission>> LoadDataTable(PaginatedDto request);
        public Task<List<PermissionTypeDto>> GetPermissionTypesAsync();
    }
}

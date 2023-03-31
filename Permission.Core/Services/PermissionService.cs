using Microsoft.EntityFrameworkCore;
using Permission.Core.DTOs;
using Permission.Core.Extensions;
using Permission.Core.Interfaces.Services;
using Permission.Core.Models;
using Permission.Core.Services.Base;
using Permission.Infra.Data.Context;
using Permission.Infra.Data.Models;
using System.Linq.Expressions;

namespace Permission.Core.Services
{
    public class PermissionService : BaseService<Permission.Infra.Data.Models.Permission>, IPermissionService
    {
        private readonly PermissionDBContext _context;

        public PermissionService(PermissionDBContext context) : base(context)
        {
            _context = context;

        }
        public async Task<PaginatedList<Permission.Infra.Data.Models.Permission>> LoadDataTable(PaginatedDto request)
        {
            Expression<Func<Permission.Infra.Data.Models.Permission, bool>> predicate = d => true;
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                predicate = d => d.Name.Contains(search);
            }

            var response = await _context.Permission
                .Where(x => x.DeletedAt == null).Where(predicate)
                .OrderBy(d => d.Name)
                .PaginatedListAsync(request);

            return response;
        }
        public async Task<Permission.Infra.Data.Models.Permission> CreatePermission(PermissionDto permissionDto)
        {

            try
            {
                Permission.Infra.Data.Models.Permission permission = new();

                permission.Name = permissionDto.Name;
                permission.LastName = permissionDto.LastName;
                permission.PermissionTypeId = permissionDto.PermissionTypeId;
                permission.CreatedAt = DateTime.Now;

                await _context.Permission.AddAsync(permission);
                await _context.SaveChangesAsync();

                return permission;
            }

            catch (Exception ex)

            {

                throw ex;
            }



        }

        public async Task<Permission.Infra.Data.Models.Permission> EditPermission(PermissionDto permissionDto)
        {

            try
            {
                Permission.Infra.Data.Models.Permission permission = await _context.Permission.FirstOrDefaultAsync(x => x.Id == permissionDto.Id);

                permission.Name = permissionDto.Name;
                permission.LastName = permissionDto.LastName;
                permission.PermissionTypeId = permissionDto.PermissionTypeId;
                permission.UpdatedAt = DateTime.Now;

                _context.Permission.Update(permission);
                await _context.SaveChangesAsync();

                return permission;
            }

            catch (Exception ex)

            {

                throw ex;
            }



        }

        public async Task<List<PermissionTypeDto>> GetPermissionTypesAsync()
        {
            return await GetAll();
        }
        private async Task<List<PermissionTypeDto>> GetAll()
        {
            List<PermissionTypeDto> result = new();

            IQueryable<PermissionType> query = _context.PermissionType;


            List<PermissionType> permissionTypes = await query.ToListAsync();
            foreach (var item in permissionTypes)
            {
                result.Add(new PermissionTypeDto
                {
                    Id = item.Id,
                    Name = item.Description

                });
            }

            return result;
        }
    }
}

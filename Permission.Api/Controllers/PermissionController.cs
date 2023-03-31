using Microsoft.AspNetCore.Mvc;
using Permission.Api.Controllers.Base;
using Permission.Core.DTOs;
using Permission.Core.Interfaces.Services;

namespace Permission.Api.Controllers
{
    public class PermissionController : ApiController
    {
        private readonly IPermissionService _service;
        private readonly ILogger<PermissionController> _logger;
        public PermissionController(IPermissionService service, ILogger<PermissionController> logger) : base(logger)
        {
            _service = service;
            _logger = logger;

        }
        [HttpPost("datatable")]
        public async Task<IActionResult> Datatable(PaginatedDto request)
        {
            try
            {
                return Response(await _service.LoadDataTable(request));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(PermissionDto entity)
        {
            try
            {
                var result = await _service.CreatePermission(entity);
                return Response(result);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit(PermissionDto entity)
        {
            try
            {
                var result = await _service.EditPermission(entity);
                return Response(result);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost("InsertGeneric")]
        public async Task<IActionResult> Insert(Permission.Infra.Data.Models.Permission entity)
        {
            try
            {
                var result = await _service.Create(entity);
                return Response(result);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut("UpdateGeneric")]
        public async Task<IActionResult> Update(Permission.Infra.Data.Models.Permission entity)
        {
            try
            {
                var result = await _service.Update(entity);
                return Response(result);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Response("Borrado Correctamente");
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }


        [HttpGet("permissionTypes")]
        public async Task<IActionResult> GetPermissionType()
        {
            try
            {
                var result = await _service.GetPermissionTypesAsync();
                return Response(result);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Permission.Core.Core.ResponseResult;

namespace Permission.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ILogger _logger;

        public ApiController(ILogger logger)
        {
            _logger = logger;
        }
        protected new IActionResult Response(string message)
        {
            return Ok(new { message });
        }

        protected new IActionResult Response(object entity = null)
        {
            if (entity != null)
                return Ok(entity);

            return NotFound(ResponseStatus.GetEnumDescription(ResponseStatus.StatusMessages.NotFoundMessage));
        }

        protected new IActionResult Response(ResponseStatus.StatusMessages status)
        {
            return Ok(new { message = ResponseStatus.GetEnumDescription(status) });
        }

        protected IActionResult Error(string message)
        {
            return BadRequest(message);
        }

        protected IActionResult Error(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest(new
            {
                message = ResponseStatus.GetEnumDescription(ResponseStatus.StatusMessages.ErrorRequest),
                errors = ex.Message
            });

            throw ex;
        }

        protected IActionResult Error(ResponseStatus.StatusMessages status)
        {
            return BadRequest(new
            {
                message = ResponseStatus.GetEnumDescription(status),
            });
        }

        protected IActionResult ErrorModelState()
        {
            return BadRequest(new
            {
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage),
                message = ResponseStatus.GetEnumDescription(ResponseStatus.StatusMessages.ErrorModelState),
            });

        }
    }
}

using Faly.Core;
using Microsoft.AspNetCore.Mvc;

namespace Faly.Api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected IActionResult ProblemDetailResult(
            int statusCode,
            string title,
            string detail,
            string instance = null
        )
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = instance ?? HttpContext.Request.Path,
            };

            return StatusCode(statusCode, problemDetails);
        }

        [NonAction]
        protected IActionResult HandleServiceResult(ServiceResult result)
        {
            if (result.Success)
            {
                if (result.Code == StatusCodes.Status201Created)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result.Code == StatusCodes.Status204NoContent)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(new { message = result.Message });
                }
            }
            else
            {
                var problemDetails = new ProblemDetails
                {
                    Status = result.Code != 0 ? result.Code : 400,
                    Title = "Bir hata oluştu",
                    Detail = result.Errors.FirstOrDefault() ?? result.Message,
                    Instance = HttpContext.Request.Path,
                };

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status.Value,
                    ContentTypes = { "application/problem+json" },
                };
            }
        }

        [NonAction]
        protected IActionResult HandleServiceResult<T>(ServiceResult<T> result)
        {
            if (result.Success)
            {
                if (result.Data == null || result.Code == StatusCodes.Status204NoContent)
                {
                    return NoContent();
                }
                else if (result.Code == StatusCodes.Status201Created)
                {
                    return Created();
                }
                else
                {
                    return Ok(result.Data);
                }
            }
            else
            {
                var problemDetails = new ProblemDetails
                {
                    Status = 400, // veya result'tan uygun bir status code alabilirsiniz
                    Title = "Bir hata oluştu",
                    Detail = result.Errors.FirstOrDefault() ?? result.Message,
                    Instance = HttpContext.Request.Path,
                };

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status.Value,
                    ContentTypes = { "application/problem+json" },
                };
            }
        }
    }
}

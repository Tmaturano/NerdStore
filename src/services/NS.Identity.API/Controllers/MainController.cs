using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NS.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected List<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);
            AddProcessingErrors(errors.Select(e => e.ErrorMessage));
            
            return CustomResponse();
        }

        protected bool IsOperationValid() => !Errors.Any();

        protected void AddProcessingError(string error) => Errors.Add(error);
        
        protected void AddProcessingErrors(IEnumerable<string> errors) => Errors.AddRange(errors);

        protected void ClearProcessingErrors() => Errors.Clear();
    }
}

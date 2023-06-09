﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NS.Core.Communication;

namespace NS.WebApi.Core.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected List<string> Errors = new List<string>();

    protected IActionResult CustomResponse(object result = null)
    {
        if (IsOperationValid())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", Errors.ToArray() }
            }));
    }

    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(x => x.Errors);
        AddProcessingErrors(errors.Select(e => e.ErrorMessage));

        return CustomResponse();
    }

    protected IActionResult CustomResponse(ValidationResult validationResult)
    {
        AddProcessingErrors(validationResult.Errors.Select(x => x.ErrorMessage));

        return CustomResponse();
    }

    protected IActionResult CustomResponse(ResponseResult response)
    {
        ResponseHasErrors(response);

        return CustomResponse();
    }

    protected bool ResponseHasErrors(ResponseResult response)
    {
        if (response is null || response.Errors.Messages.Any()) return false;

        foreach (var error in response.Errors.Messages)
            AddProcessingError(error);

        return true;
    }

    protected bool IsOperationValid() => !Errors.Any();

    protected void AddProcessingError(string error) => Errors.Add(error);

    protected void AddProcessingErrors(IEnumerable<string> errors) => Errors.AddRange(errors);

    protected void ClearProcessingErrors() => Errors.Clear();
}

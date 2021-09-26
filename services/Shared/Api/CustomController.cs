using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Api
{
    [ApiController]
    [Route("[controller]")]
    public class CustomController : Controller
    {
        protected IActionResult CustomReponse(ValidationResult validationResult, object result = null)
        {
            if (OperacaoValida(validationResult.Errors))
                return result is null ? Ok() : Ok(result);

            return BadRequest(validationResult.Errors);
        }

        private bool OperacaoValida(List<ValidationFailure> errors) => !errors.Any();
    }


}
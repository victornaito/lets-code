using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Shared.Api
{
    public abstract class CommandBase
    {
        protected ValidationResult ValidationResult { get; private set; }
        public ValidationResult Validation() { return ValidationResult; }
        public void SetValidation(ValidationResult validationResult) { ValidationResult = validationResult; }
        public abstract bool IsValid();
    }
}
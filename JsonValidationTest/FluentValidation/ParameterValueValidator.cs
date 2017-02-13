using FluentValidation;
using JsonValidationTest.Representation;

namespace JsonValidationTest.Fluent
{
  internal class ParameterValueValidator : AbstractValidator<ParameterValue>
  {
		public ParameterValueValidator()
    {
      RuleFor(v => v.Value).NotNull();
    }
  }
}

using FluentValidation;
using JsonValidationTest.Representation;

namespace JsonValidationTest.Fluent
{
  internal class ParameterValidator : AbstractValidator<Parameter>
  {
    public ParameterValidator()
    {
      RuleFor(p => p.Name).NotEmpty();
      RuleFor(p => p.SelectedValues).NotNull();
			RuleFor(p => p.SelectedValues).SetCollectionValidator(new ParameterValueValidator());
    }
  }
}

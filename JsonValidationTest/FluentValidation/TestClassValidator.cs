using FluentValidation;
using JsonValidationTest.Extensions;
using JsonValidationTest.Representation;

namespace JsonValidationTest.Fluent
{
	public class TestClassValidator : AbstractValidator<TestClass>
	{
    public TestClassValidator()
    {
      // Not null
      RuleFor(t => t.NonNullableText).NotNull();

      // Not empty
      RuleFor(t => t.NotEmptyText).NotEmpty();

      // Length between 1 and 10
      RuleFor(t => t.MinLenText).Length(1, 10);

      // Option must be same value as ValidOption (but only if MustBeValidOption is true)
      RuleFor(t => t.Option).Equal(t => t.ValidOption).When(t => t.MustBeValidOption);

      // List must contain at least 2 items (applicable to all Lists/Arrays)
      RuleFor(t => t.TextItems).NotNull().MustContainAtLeast(2);

      // Date must arrive as UTC (ISO with "Z")
      RuleFor(t => t.UtcDate).NotNull().MustBeUtc();
    }
	}
}

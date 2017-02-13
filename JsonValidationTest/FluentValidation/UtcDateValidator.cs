using System;
using FluentValidation.Validators;

namespace JsonValidationTest.Fluent
{
  public class UtcDateValidator : PropertyValidator
  {
    public UtcDateValidator() : base("'{PropertyName}' is not an UTC date")
    {
    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
      DateTime? dateTime = context.PropertyValue as DateTime?;
      if (dateTime != null)
      {
        return dateTime.Value.Kind == DateTimeKind.Utc;
      }

      return true;
    }
  }
}

using FluentValidation.Validators;
using System.Collections.Generic;

namespace JsonValidationTest.Fluent
{
  public class ListMustContainAtLeastValidator<T> : PropertyValidator
  {
    private readonly int _minCount;

    public ListMustContainAtLeastValidator(int minCount) 
      : base("'{PropertyName}'  contains less than " + minCount + " item(s).")
    {
      _minCount = minCount;
    }

    protected override bool IsValid(PropertyValidatorContext context)
    {
      IList<T> list = context.PropertyValue as IList<T>;
      if (list != null && list.Count < _minCount)
      {
        return false;
      }

      return true;
    }
  }
}

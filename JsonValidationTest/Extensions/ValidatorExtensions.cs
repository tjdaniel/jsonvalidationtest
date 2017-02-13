using FluentValidation;
using JsonValidationTest.Fluent;
using System;
using System.Collections.Generic;

namespace JsonValidationTest.Extensions
{
  public static class ValidatorExtensions
  {
    public static IRuleBuilderOptions<T, IList<TElement>> MustContainAtLeast<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int minCount)
    {
      return ruleBuilder.SetValidator(new ListMustContainAtLeastValidator<TElement>(minCount));
    }

    public static IRuleBuilderOptions<T, DateTime?> MustBeUtc<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
      return ruleBuilder.SetValidator(new UtcDateValidator());
    }

    public static IRuleBuilderOptions<T, DateTime> MustBeUtc<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
      return ruleBuilder.SetValidator(new UtcDateValidator());
    }
  }
}
